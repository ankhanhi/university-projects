#include <stdio.h>
#include "mpi.h"
#include <random>
#include <iostream>

int generate_int(int min_v, int max_v) {
   return min_v + std::rand() % (max_v - min_v);
}

int* merge(int* arr1, int n1, int* arr2, int n2)
{
	int* arr3 = new int[n1 + n2];
	int i = 0, j = 0, k = 0;

	// Traverse both array
	while ((i < n1) && (j < n2))
	{
		if (arr1[i] < arr2[j])
			arr3[k++] = arr1[i++];
		else
			arr3[k++] = arr2[j++];
	}

	// Store remaining elements of first array
	while (i < n1)
		arr3[k++] = arr1[i++];

	// Store remaining elements of second array
	while (j < n2)
		arr3[k++] = arr2[j++];

	// sort the whole array arr3
	std::sort(arr3, arr3 + n1 + n2);

	return arr3;
}

void merge_sort(int* arr, int start, int end)
{
	if (end > start) {
		int mid = (start + end) / 2;
		int left = mid - start + 1;
		int right = end - mid;
		merge_sort(arr, start, mid);
		merge_sort(arr, mid + 1, end);
		merge(arr + start, left, arr + mid + 1, right);
	}
}


int main(int* argc, char** argv)
{
	srand(time(0));

	int MIN_NUM = 0;
	int MAX_NUM = 10000;
	int SIZE_ARRAY = 100;
	int recvcount = SIZE_ARRAY;
	int ProcRank, ProcNum;
	MPI_Status Status;
	MPI_Init(argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &ProcRank);
	MPI_Comm_size(MPI_COMM_WORLD, &ProcNum);

	MPI_Comm comm1;
	MPI_Comm comm2;
	MPI_Comm comm3;
	MPI_Comm comm4;
	MPI_Comm comm5;
	MPI_Comm comm6;
	MPI_Comm comm7;

	MPI_Group group;
	MPI_Group group1;
	MPI_Group group2;
	MPI_Group group3;
	MPI_Group group4;
	MPI_Group group5;
	MPI_Group group6;
	MPI_Group group7;

	MPI_Comm_group(MPI_COMM_WORLD, &group);

	const int ranks[6] = { 1, 2, 3, 4, 5, 6 };
	const int ranks1[2] = { 1, 2 };
	const int ranks2[2] = { 3, 4 };
	const int ranks3[2] = { 5, 6 };
	const int ranks4[2] = { 1, 3 };
	const int ranks5[2] = { 1, 5 };
	const int ranks6[2] = { 0, 1 };

	MPI_Group_incl(group, 6, ranks, &group1);
	MPI_Group_incl(group, 2, ranks1, &group2);
	MPI_Group_incl(group, 2, ranks2, &group3);
	MPI_Group_incl(group, 2, ranks3, &group4);
	MPI_Group_incl(group, 2, ranks4, &group5);
	MPI_Group_incl(group, 2, ranks5, &group6);
	MPI_Group_incl(group, 2, ranks6, &group7);

	MPI_Comm_create(MPI_COMM_WORLD, group1, &comm1);
	MPI_Comm_create(MPI_COMM_WORLD, group2, &comm2);
	MPI_Comm_create(MPI_COMM_WORLD, group3, &comm3);
	MPI_Comm_create(MPI_COMM_WORLD, group4, &comm4);
	MPI_Comm_create(MPI_COMM_WORLD, group5, &comm5);
	MPI_Comm_create(MPI_COMM_WORLD, group6, &comm6);
	MPI_Comm_create(MPI_COMM_WORLD, group7, &comm7);

	int n_part = SIZE_ARRAY / (ProcNum - 1)+1; 
	int r = SIZE_ARRAY % (ProcNum - 1);
	int n_r = SIZE_ARRAY + (ProcNum - 1) - r;
	int* data_array = new int [n_r];
	for (int i = 0; i < r; i++)
	{
		data_array[i] = 0;
	}
	for (int i = r; i < n_r; i++)
	{
		data_array[i] = generate_int(MIN_NUM, MAX_NUM);
	}
	if (ProcRank == 0)
	{
		printf("Unsorted:\n");
		for (int i = r; i < n_r; i++) {
			printf("%d ", data_array[i]);
		}
		printf("\n");
		for (int i = 1; i < ProcNum; i++)
		{
			// 0 -> 1, 2, 3, 4, 5, 6
			MPI_Send(&n_part, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
		}
	}
	else
	{
		//1, 2, 3, 4, 5, 6 <- 0
		MPI_Recv(&n_part, 1, MPI_INT, 0, 0, MPI_COMM_WORLD, &Status);
		int* arr_part = new int[n_part];
		//data -> 1, 2, 3, 4, 5, 6
		MPI_Scatter(data_array, n_part, MPI_INT, arr_part, n_part, MPI_INT, 1, comm1);
		

		if (ProcRank == 1)
		{
			//1 <- 2
			MPI_Bcast(&recvcount, 1, MPI_INT, 1, comm2);
			int* recvArr = new int[recvcount];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, recvcount, MPI_INT, 1, comm2);
			arr_part = merge(arr_part, n_part, recvArr, recvcount);
			n_part += recvcount;

			//1 <- 3
			MPI_Bcast(&recvcount, 1, MPI_INT, 1, comm5);
			recvArr = new int[recvcount];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, recvcount, MPI_INT, 1, comm5);
			arr_part = merge(arr_part, n_part, recvArr, recvcount);
			n_part += recvcount;

			//1 <- 5
			MPI_Bcast(&recvcount, 1, MPI_INT, 1, comm6);
			recvArr = new int[recvcount];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, recvcount, MPI_INT, 1, comm6);
			arr_part = merge(arr_part, n_part, recvArr, recvcount);
			n_part += recvcount;
			//1 -> 0
			MPI_Send(&n_part, 1, MPI_INT, 0, 0, comm7);
			MPI_Send(arr_part, n_part, MPI_INT, 0, 0, comm7);
		}
		if (ProcRank == 2)
		{
			//2 -> 1
			MPI_Bcast(&n_part, 1, MPI_INT, 1, comm2);
			int* recvArr = new int [n_part];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, n_part, MPI_INT, 1, comm2);
		}
		if (ProcRank == 3)
		{
			// 3 <- 4
			MPI_Bcast(&recvcount, 1, MPI_INT, 1, comm3);
			int* recvArr = new int[n_part];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, recvcount, MPI_INT, 1, comm3);
			arr_part = merge(arr_part, n_part, recvArr, recvcount);
			n_part += recvcount;
			printf("-----%d-", recvcount);
			//3 -> 1
			MPI_Bcast(&n_part, 1, MPI_INT, 1, comm5);
			recvArr = new int[n_part];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, n_part, MPI_INT, 1, comm5);
		}
		if (ProcRank == 4)
		{
			//4 -> 3
			MPI_Bcast(&n_part, 1, MPI_INT, 1, comm3);
			int* recvArr = new int[n_part];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, n_part, MPI_INT, 1, comm3);
		}
		if (ProcRank == 5)
		{
			//5 <- 6
			MPI_Bcast(&recvcount, 1, MPI_INT, 1, comm4);
			int* recvArr = new int[recvcount];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, recvcount, MPI_INT, 1, comm4);
			arr_part = merge(arr_part, n_part, recvArr, recvcount);
			n_part += recvcount;
			//5 -> 1
			MPI_Bcast(&n_part, 1, MPI_INT, 1, comm6);
			recvArr = new int[n_part];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, n_part, MPI_INT, 1, comm6);
		}
		if (ProcRank == 6)
		{
			//6 -> 5
			MPI_Bcast(&n_part, 1, MPI_INT, 1, comm4);
			int* recvArr = new int[n_part];
			MPI_Scatter(arr_part, n_part, MPI_INT, recvArr, n_part, MPI_INT, 1, comm4);
		}
	}
	if (ProcRank == 0)
	{
		//0 <- 1 
		MPI_Recv(&recvcount, 1, MPI_INT, 1, 0, comm7, &Status);
		MPI_Recv(data_array, recvcount, MPI_INT, 1, 0, comm7, &Status);
		printf("Sorted:\n");
		for (int i = r; i < n_r; i++) {
			printf("%d ", data_array[i]);
		}
		printf("\n");
	}
	MPI_Finalize();
    return 0;
}