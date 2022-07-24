Shader "Hidden/CrtPostProcess"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth 关闭剔除、关闭深度写入
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			//自定义一个顶点数据
			struct appdata
			{
				float4 vertex : POSITION;	//获得顶点数据
				float2 uv : TEXCOORD0;		//获得纹理坐标数据
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			//声明uniform,说明以下变量需要由应用程序提供给它，如果没有提供给它，默认的是0
			uniform float u_time;
			uniform float u_bend;
			uniform float u_scanline_size_1;
			uniform float u_scanline_speed_1;
			uniform float u_scanline_size_2;
			uniform float u_scanline_speed_2;
			uniform float u_scanline_amount;
			uniform float u_vignette_size;
			uniform float u_vignette_smoothness;
			uniform float u_vignette_edge_round;
			uniform float u_noise_size;
			uniform float u_noise_amount;
			uniform half2 u_red_offset;
			uniform half2 u_green_offset;
			uniform half2 u_blue_offset;

			//添加具有crt屏幕的圆润感的效果的结构体
			half2 crt_coords(half2 uv, float bend)
			{
				//uv坐标转换成-1到1之间的区间方便后续转换
				uv -= 0.5;
				uv *= 2.;

				//将坐标曲线弯曲的功能 pow(x,y)即x的y次方
				uv.x *= 1. + pow(abs(uv.y) / bend, 2.);
				uv.y *= 1. + pow(abs(uv.x) / bend, 2.);

				//将坐标重新返回至0到1的区间
				uv /= 2.5;
				return uv + 0.5;
			}

			//添加小插图
			float vignette(half2 uv, float size, float smoothness, float edgeRounding)
			{
				uv -= .5;
				uv *= size;

				//此处使用的是距离公式
				float amount = sqrt(pow(abs(uv.x), edgeRounding) + pow(abs(uv.y), edgeRounding));
				amount = 1. - amount;

				//smoothstep()平滑过渡函数
				return smoothstep(0, smoothness, amount);
			}

			//扫描线效果
			float scanline(half2 uv, float lines, float speed)
			{
				//控制扫描线的数量以及扫描线的运动
				return sin(uv.y * lines + u_time * speed);
			}

			//随机函数
			float random(half2 uv)
			{
				//随机函数，该数据为作者测试效果
				return frac(sin(dot(uv, half2(15.1511, 42.5225))) * 12341.51611 * sin(u_time * 0.03));
			}

			float noise(half2 uv)
			{
				//floor()返回等于或小于的最接近整数的值
				half2 i = floor(uv);
				half2 f = frac(uv);

				//设置噪点中那些2D方块中的四个角
				float a = random(i);
				float b = random(i + half2(1., 0.));
				float c = random(i + half2(0, 1.));
				float d = random(i + half2(1., 1.));

				half2 u = smoothstep(0., 1., f);

				//混合方块四个角的百分比
				return lerp(a, b, u.x) + (c - a) * u.y * (1. - u.x) + (d - b) * u.x * u.y;
			}

			//输出
			fixed4 frag(v2f i) : SV_Target
			{
				half2 crt_uv = crt_coords(i.uv, u_bend);
				fixed4 col;
				col.r = tex2D(_MainTex, crt_uv + u_red_offset).r;
				col.g = tex2D(_MainTex, crt_uv + u_green_offset).g;
				col.b = tex2D(_MainTex, crt_uv + u_blue_offset).b;
				col.a = tex2D(_MainTex, crt_uv).a;

				float s1 = scanline(i.uv, u_scanline_size_1, u_scanline_speed_1);
				float s2 = scanline(i.uv, u_scanline_size_2, u_scanline_speed_2);

				col = lerp(col, fixed(s1 + s2), u_scanline_amount);

				return lerp(col, fixed(noise(i.uv * u_noise_size)), u_noise_amount) * vignette(i.uv, u_vignette_size, u_vignette_smoothness, u_vignette_edge_round);
			}
			ENDCG
		}
	}
}