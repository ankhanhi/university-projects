/*
На основе трёх равно размерных массивов A, B и C (длины N) функция возвращает произведение ненулевых значений,
полученных таким образом: если Ai четно: Bi/Ci , иначе Ai+Bi
*/
#include <omp.h>
#include <iostream>
#include <ctime>
#include <iomanip>

// Возвращает псевдослучайное число из диапазона [min, max)
int Random(int min, int max) {
	return min + rand() % (max - min);
}

//Создание рандомного массива
int* generateArr(int n) {
	//Автоматическая рандомизация
	srand((unsigned int)time(NULL));

	int* arr = new int[n];
	for (int i = 0; i < n; i++) {
		arr[i] = Random(1, 100);
	}
	return arr;
}

int sub_omp_2(int n, int* A, int* B, int* C) {
	int result = 1;
	int i, j, k;
#pragma omp parallel sections for shared(A,B,C) private(i)
	{
#pragma omp critical
	for (i = 0; i < n / 2; i++) {
		if (A[i] % 2 == 0) {
			result *= B[i] / C[i];
		}
		else {
			result *= (A[i] + B[i]);
		}
	}
#pragma omp critical
	for (i = n / 2; i < n; i++) {
		if (A[i] % 2 == 0) {
			result *= B[i] / C[i];
		}
		else {
			result *= (A[i] + B[i]);
		}
	}
	}
	return result;
}

int sub_omp_4(int n, int* A, int* B, int* C) {
	int result = 1;
	int i, j, k;

#pragma omp parallel sections for shared(A,B,C) private(i)
	{
#pragma omp critical
		for (i = 0; i < n / 4; i++) {
			if (A[i] % 2 == 0) {
				result *= B[i] / C[i];
			}
			else {
				result *= (A[i] + B[i]);
			}
		}
#pragma omp critical
		for (i = n / 4; i < n / 2; i++) {
			if (A[i] % 2 == 0) {
				result *= B[i] / C[i];
			}
			else {
				result *= (A[i] + B[i]);
			}
		}
#pragma omp critical
		for (i = n / 2; i < 3 * n / 4; i++) {
			if (A[i] % 2 == 0) {
				result *= B[i] / C[i];
			}
			else {
				result *= (A[i] + B[i]);
			}
		}
#pragma omp critical
		for (i = 3 * n / 4; i < n; i++) {
			if (A[i] % 2 == 0) {
				result *= B[i] / C[i];
			}
			else {
				result *= (A[i] + B[i]);
			}
		}
	}
	return result;
}

void test_omp_2(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//Расчет произведения по данным условиям
	t1 = omp_get_wtime();
	result = sub_omp_2(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "Тест c 2 секциями" << std::endl;
	std::cout << "Произведение: " << result << std::endl;
	std::cout << "Время исполнения основного вычислительного блока: " << t2 - t1 << std::endl;
}
void test_omp_4(int n, int* A, int* B, int* C) {
	double t1, t2, result;
	//Расчет произведения по данным условиям
	t1 = omp_get_wtime();
	result = sub_omp_4(n, A, B, C);
	t2 = omp_get_wtime();
	std::cout << "Тест c 4 секциями" << std::endl;
	std::cout << "Произведение: " << result << std::endl;
	std::cout << "Время исполнения основного вычислительного блока: " << t2 - t1 << std::endl;
}

int main() {

	system("chcp 1251");

	int n;

	while (true) {
		//Ввод количества строк и столбцов массива
		std::cout << "Введите размер массива n:" << std::endl;
		std::cin >> n;
		int* A = generateArr(n);
		int* B = generateArr(n);
		int* C = generateArr(n);

		test_omp_2(n, A, B, C);
		test_omp_4(n, A, B, C);
	}


	return 0;
}