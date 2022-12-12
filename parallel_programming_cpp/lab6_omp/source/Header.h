#pragma once
#include <omp.h>
#include <iostream>
#include <ctime>
#include <iomanip>
class Sub
{
public:
	int sub(int n, int* A, int* B, int* C);
	int sub_omp_lock(int n, int* A, int* B, int* C);
	void test(int n, int* A, int* B, int* C);
	void test_omp_lock(int n, int* A, int* B, int* C);

	int sub_omp_barrier(int n, int* A, int* B, int* C);
	void test_omp_barrier(int n, int* A, int* B, int* C);
};