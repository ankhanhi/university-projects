/*
��������� ������ ��� ��������� �������� ���������� NMAX � LIMIT, 
������� ����� ����������, ���������� ������� � �����. 
�������� ����� NMAX ��� LIMIT, ��� ������� ��������� ����� ���������� ������������� ��������� � ������������.
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
    }/* ���������� ������������� ��������� */
    t2 = omp_get_wtime();
    printf("����� ����������: %f\n", t2 - t1);
    t1 = omp_get_wtime();
    {
        for (i = 0; i < NMAX; i++)
        {
            sum = 0;
            for (j = 0; j < NMAX; j++)
                sum += a[i][j];
        }
    }/* ���������� ������������� ��������� */
    t2 = omp_get_wtime();
    printf("����� ���������� ��� omp: %f\n", t2 - t1);
}




