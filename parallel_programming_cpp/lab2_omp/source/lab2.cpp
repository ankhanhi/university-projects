/*
Выполнить Пример для различных значений параметров NMAX и LIMIT, 
замеряя время выполнения, результаты занести в отчет. 
Выделить такие NMAX при LIMIT, при которых совпадает время выполнения многопоточной программы и однопоточной.
*/

#include <omp.h>
#include <stdio.h>
#include <iomanip>
#include <ctime>
const int NMAX = 400;


void main()
{
    system("chcp 1251");
    double t1, t2;
    int i, j;
    float sum;
    float a[NMAX][NMAX];

    for (i = 0; i < NMAX; i++)
        for (j = 0; j < NMAX; j++) {
            a[i][j] = i + j;
        }
            
    t1 = omp_get_wtime();
    #pragma omp parallel shared(a)
    {
    #pragma omp for private(i,j,sum) 
    for (i = 0; i < NMAX; i++)
    {
        sum = 0;
        for (j = 0; j < NMAX; j++)
            sum += a[i][j];
    }
    }/* Завершение параллельного фрагмента */
    t2 = omp_get_wtime();
    printf("Время выполнения: %f\n", t2 - t1);
    t1 = omp_get_wtime();
    {
        for (i = 0; i < NMAX; i++)
        {
            sum = 0;
            for (j = 0; j < NMAX; j++)
                sum += a[i][j];
        }
    }/* Завершение параллельного фрагмента */
    t2 = omp_get_wtime();
    printf("Время выполнения без omp: %f\n", t2 - t1);
}




