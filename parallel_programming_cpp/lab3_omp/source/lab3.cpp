/*
�� ������ ��� ����� ��������� �������� A, B � C (����� N) ������� ���������� ������������ ��������� ��������, 
���������� ����� �������: ���� Ai �����: Bi/Ci , ����� Ai+Bi  
*/
#include <omp.h>
#include <iostream>
#include <ctime>
#include <iomanip>

// ���������� ��������������� ����� �� ��������� [min, max)
int Random(int min, int max) {
	return min + rand() % (max - min);
}

//�������� ���������� �������
int* generateArr(int n) {
	//�������������� ������������
	srand((unsigned int)time(NULL)); 

	int* arr = new int[n];
	for (int i = 0; i < n; i++) {
		arr[i] = Random(1, 100);
	}
	return arr;
}

int sub(int n, int* A, int* B, int* C) {
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

int sub_omp(int n, int* A, int* B, int* C) {
	int result = 1;
	int i, j, k;

#pragma omp parallel for shared(A,B,C) private(i) reduction(*:result) 
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

void test(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//������ ������������ �� ������ ��������
	t1 = omp_get_wtime();
	result = sub(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "���� ��� ������������� OpenMP" << std::endl;
	std::cout << "������������: " << result << std::endl;
	std::cout << "����� ���������� ��������� ��������������� �����: " << t2 - t1 << std::endl;
}

void test_omp(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//������ ������������ �� ������ ��������
	t1 = omp_get_wtime();
	result = sub_omp(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "���� c �������������� OpenMP" << std::endl;
	std::cout << "������������: " << result << std::endl;
	std::cout << "����� ���������� ��������� ��������������� �����: " << t2 - t1 << std::endl;
}

int main() {
	
	system("chcp 1251");

	int n;

	while (true) {
		//���� ���������� ����� � �������� �������
		std::cout << "������� ������ ������� n:" << std::endl;
		std::cin >> n;
		int* A = generateArr(n);
		int* B = generateArr(n);
		int* C = generateArr(n);

		test(n, A, B, C);
		test_omp(n, A, B, C);
	}
	
	
	return 0;
}