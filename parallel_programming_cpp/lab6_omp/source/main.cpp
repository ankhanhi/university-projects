/*
На основе трёх равно размерных массивов A, B и C (длины N) функция возвращает произведение ненулевых значений,
полученных таким образом: если Ai четно: Bi/Ci , иначе Ai+Bi
*/
#include "Header.h"


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

int main() {

	system("chcp 1251");

	int n;
	Sub sub0;
	while (true) {
		//Ввод количества строк и столбцов массива
		std::cout << "Введите размер массива n:" << std::endl;
		std::cin >> n;
		int* A = generateArr(n);
		int* B = generateArr(n);
		int* C = generateArr(n);

		sub0.test(n, A, B, C);
		sub0.test_omp_lock(n, A, B, C);
		sub0.test_omp_barrier(n, A, B, C);
	}


	return 0;
}