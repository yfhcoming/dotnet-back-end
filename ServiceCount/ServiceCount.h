#pragma once

using namespace System;

namespace ServiceCount {
	public ref class CountClass
	{
		// TODO: 在此处为此类添加方法。


	public:
		template<typename T> static void swap(T& a, T& b) {
			T temp;
			temp = a;
			a = b;
			b = temp;
		}

		static int addOne(int num) {
			return num + 1;
		}



		static int add(int a, int b) {
			return a + b;
		}

		static void swap2(int* a, int* b) {
			int temp = *a;
			*a = *b;
			*b = temp;
		}
	};
}
