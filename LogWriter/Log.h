#pragma once
#include <ctime>
#include <stdlib.h>
#include <string>
#include <string>

extern "C" __declspec(dllexport) int add(int a, int b);
extern "C" __declspec(dllexport) int del(int a, int b);

extern "C" __declspec(dllexport) char printLog();
