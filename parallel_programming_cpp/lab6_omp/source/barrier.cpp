#include "Header.h"

// ѕо заданию требуетс€ использовать барьеризацию дл€ оптимизации кода, однако,
// € не пон€ла, каким образом барьеры здесь ускор€т программу, поэтому использовала
// директиву barrier чисто дл€ демонстрации
int Sub::sub_omp_barrier(int n, int* A, int* B, int* C) {
	int result = 1;
	int i, j, k;

#pragma omp parallel for shared(A,B,C) private(i)  
	for (i = 0; i < n; i++) {
		if (A[i] % 2 == 0) {
#pragma omp barrier
			result *= B[i] / C[i];
		}
		else {
#pragma omp barrier
			result *= (A[i] + B[i]);
		}
	}
	return result;
}


void Sub::test_omp_barrier(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//–асчет произведени€ по данным услови€м
	t1 = omp_get_wtime();
	result = sub_omp_barrier(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "“ест c барьеризацией" << std::endl;
	std::cout << "ѕроизведение: " << result << std::endl;
	std::cout << "¬рем€ исполнени€ основного вычислительного блока: " << t2 - t1 << std::endl;
}



