#include "Header.h"


int Sub::sub(int n, int* A, int* B, int* C) {
	int result = 1;
	int i, j, k;

	for (i = 0; i < n; i++) {
		if (A[i] % 2 == 0) {
			result *= B[i] / C[i];
		}
		else {
			result *= (A[i] + B[i]);
		}
	}
	return result;
}



void Sub::test(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//Расчет произведения по данным условиям
	t1 = omp_get_wtime();
	result = sub(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "Тест без использования OpenMP" << std::endl;
	std::cout << "Произведение: " << result << std::endl;
	std::cout << "Время исполнения основного вычислительного блока: " << t2 - t1 << std::endl;
}

