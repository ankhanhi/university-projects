#include "Header.h"


int Sub::sub_omp_lock(int n, int* A, int* B, int* C) {
	omp_lock_t lock;
	omp_init_lock(&lock);
	int result = 1;
	int i, j, k;

#pragma omp parallel for shared(A,B,C) private(i) reduction(*:result) 
	for (i = 0; i < n; i++) {
		if (A[i] % 2 == 0) {
			omp_set_lock(&lock);
			result *= B[i] / C[i];
			omp_unset_lock(&lock);
		}
		else {
			omp_set_lock(&lock);
			result *= (A[i] + B[i]);
			omp_unset_lock(&lock);
		}
	}
	return result;
}
void Sub::test_omp_lock(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//������ ������������ �� ������ ��������
	t1 = omp_get_wtime();
	result = sub_omp_lock(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "���� c ������" << std::endl;
	std::cout << "������������: " << result << std::endl;
	std::cout << "����� ���������� ��������� ��������������� �����: " << t2 - t1 << std::endl;
}
