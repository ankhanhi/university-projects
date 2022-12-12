#include <stdio.h>
#include "mpi.h"
#include <iostream>
#include <time.h>
#include <locale>
using namespace std;

int generate_int(int min_v, int max_v) {
   return min_v + rand() % (max_v - min_v);
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
   int counter = 0;
   int rand_send = 0;
   srand(time(0));
  
   if (ProcRank == 0)
   {
       while (flag) {
           for (int i = 1; i < ProcNum; i++)
           {
               MPI_Recv(&RecvRank, 1, MPI_INT, i, MPI_ANY_TAG, MPI_COMM_WORLD, &Status);
               
               if (RecvRank == -1) {
                   flag = false;
                   MPI_Send(&flag, 1, MPI_C_BOOL, Status.MPI_SOURCE, 0, MPI_COMM_WORLD);
                   printf("\n Exit the loop. Counter value: %3d", counter);
               }
               else 
               {
                   counter++;
                   printf("\n The counter has been increased: %3d", counter);
                   MPI_Send(&flag, 1, MPI_C_BOOL, Status.MPI_SOURCE, 0, MPI_COMM_WORLD);
               }

           }
       }
   }
   else {
       while (flag) {
           rand_send = generate_int(-1,7);          
           MPI_Send(&rand_send, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
           printf("\n Random value sent to 0 process: %d", rand_send);
           MPI_Recv(&flag, 1, MPI_C_BOOL, 0, 0, MPI_COMM_WORLD, &Status);
           printf("\n Flag information accepted");
       }
   }
   MPI_Finalize();
   return 0;
}

