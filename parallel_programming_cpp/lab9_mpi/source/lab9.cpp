#include <stdio.h>
#include "mpi.h"
#include <iostream>
#include <time.h>
#include <locale>
#include <vector>
using namespace std;

int generate_int(int min_v, int max_v) {
   return min_v + std::rand() % (max_v - min_v);
}

int main(int argc, char** argv)
{

   int ProcNum, ProcRank;
   int RecvRank = 0;

   MPI_Init(&argc, &argv);
   MPI_Comm_rank(MPI_COMM_WORLD, &ProcRank);
   MPI_Comm_size(MPI_COMM_WORLD, &ProcNum);

   MPI_Status Status;
   bool flag = true;
   bool flag_vec = true;
   int counter = 0;
   int rand_send = 0;
   int rand_recv[8];
   for (int i = 0; i < 8; i++)
   {
       rand_recv[i] = 0;
   }

   srand(time(0));

   while (flag) {
       if (ProcRank != 0) {
           rand_send = generate_int(-1, 15);
           MPI_Gather(&rand_send, 1, MPI_INT, rand_recv, 1, MPI_INT, 0, MPI_COMM_WORLD);
       }
       else {
           MPI_Gather(&rand_send, 1, MPI_INT, rand_recv, 1, MPI_INT, 0, MPI_COMM_WORLD);
           for (int i = 0; i < 8; i++)
           {
               printf("%d-", rand_recv[i]);
           }
           for (int i = 0; i < 8; i++)
           {
               if (rand_recv[i] == -1) {
                   printf("\n THE END");
                   flag = false;
                   break;
               }
               else {
                   printf("\n %d", rand_recv[i]);
               }
           }


       }
       MPI_Bcast(&flag, 1, MPI_C_BOOL, 0, MPI_COMM_WORLD);

   }



   MPI_Finalize();


   return 0;
}