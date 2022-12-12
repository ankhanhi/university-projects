#include "Header.h"

// �� ������� ��������� ������������ ������������ ��� ����������� ����, ������,
// � �� ������, ����� ������� ������� ����� ������� ���������, ������� ������������
// ��������� barrier ����� ��� ������������
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
	//������ ������������ �� ������ ��������
	t1 = omp_get_wtime();
	result = sub_omp_barrier(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "���� c �������������" << std::endl;
	std::cout << "������������: " << result << std::endl;
	std::cout << "����� ���������� ��������� ��������������� �����: " << t2 - t1 << std::endl;
}



