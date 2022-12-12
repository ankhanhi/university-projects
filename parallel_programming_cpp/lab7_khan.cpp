

/*
���������� �� ������ ���������� MPI ��������������� ��������� � ��������� ����� ������,
� ������� �������������� ����� ����������� �� ������ ��� ������ ������ � ���������� ���������� ���������,
� ������ ������, � ������� ������� �������������� �� ����� master-slave(������) �� ���� ��������� �����.
*/

#include "mpi.h"
#include <stdio.h>

int main(int argc, char* argv[])
{
    int size, rank;

    MPI_Init(&argc, &argv);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Group group;
    MPI_Group StarGroup;
    MPI_Group RingGroup;
    MPI_Comm StarComm;
    MPI_Comm RingComm;

   

    //MPI_Comm_group(MPI_COMM_WORLD, &group);

    //const int ranks[5] = { 0, 1, 2, 3, 4 };

    //MPI_Group_incl(group, 5, ranks, &StarGroup);
    //MPI_Comm_create(MPI_COMM_WORLD, StarGroup, &StarComm);

    /* �������� ���������� ��������� ���������� ���������*/
    /* �������� ��������� ���� ������ */
    int index[] = { 4,5,6,7,8 };
    int edges[] = { 1,2,3,4,0,0,0,0 };
    
    MPI_Graph_create(MPI_COMM_WORLD, 5, index, edges, 1, &StarComm);
    int count;

    MPI_Graph_neighbors_count(StarComm, rank, &count);
    printf("MPI_Graph_neighbors_count: %d\n", count);

    int* neighbors = new int[count];

    MPI_Graph_neighbors(StarComm, rank, count, neighbors);

    int a = rank;
    int b;
    int buf[2];
    MPI_Status status1;
    MPI_Status status2;

    for (int i = 0; i < count; ++i)
    {

        MPI_Sendrecv(&a, 1, MPI_INT, neighbors[i], 0, &b, 1, MPI_INT, neighbors[i], 0, StarComm, &status1);
        printf("a: %d, b: %d, Rank: %d, neigh: %d\n", a, b, rank, neighbors[i]);

    }
    //

    MPI_Finalize();
    return 0;
}

