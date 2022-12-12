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
   MPI_Datatype polynom_type;

   MPI_Status Status;
   int size_p = 5;

   int tag1 = 1;
   int tag2 = 2;
   int tag3 = 3;
   int tag4 = 4;
   int tag5 = 5;
   int tag6 = 6;
   int tag7 = 7;
   int tag8 = 8;
   int tag11 = 11;
   int tag21 = 21;
   int tag31 = 31;
   int tag41 = 41;

   srand(time(0));

   MPI_Type_contiguous(8*size_p-7, MPI_INT, &polynom_type);
   MPI_Type_commit(&polynom_type);
   if (ProcRank == 0) {
       int* pol1 = new int[8*size_p-7];
       int* pol2 = new int[8 * size_p - 7];
       int* pol3 = new int[8 * size_p - 7];
       int* pol4 = new int[8 * size_p - 7];
       int* pol5 = new int[8 * size_p - 7];
       int* pol6 = new int[8 * size_p - 7];
       int* pol7 = new int[8 * size_p - 7];
       int* pol8 = new int[8 * size_p - 7];
       int* pol12 = new int[8 * size_p - 7];

       for (int i = 0; i < 8 * size_p - 7; i++) {
           pol1[i] = 0;
           pol2[i] = 0;
           pol3[i] = 0;
           pol4[i] = 0;
           pol5[i] = 0;
           pol6[i] = 0;
           pol7[i] = 0;
           pol8[i] = 0;
           pol12[i] = 0;
       }

       for (int i = 0; i < size_p; i++) {
           pol1[i] = generate_int(1, 3);
           pol2[i] = generate_int(1, 3);
           pol3[i] = generate_int(1, 3);
           pol4[i] = generate_int(1, 3);
           pol5[i] = generate_int(1, 3);
           pol6[i] = generate_int(1, 3);
           pol7[i] = generate_int(1, 3);
           pol8[i] = generate_int(1, 3);
       }


       MPI_Send(&pol1[0], 1, polynom_type, 1, tag1, MPI_COMM_WORLD);
       MPI_Send(&pol2[0], 1, polynom_type, 1, tag2, MPI_COMM_WORLD);
       MPI_Send(&pol3[0], 1, polynom_type, 2, tag3, MPI_COMM_WORLD);
       MPI_Send(&pol4[0], 1, polynom_type, 2, tag4, MPI_COMM_WORLD);
       MPI_Send(&pol5[0], 1, polynom_type, 3, tag5, MPI_COMM_WORLD);
       MPI_Send(&pol6[0], 1, polynom_type, 3, tag6, MPI_COMM_WORLD);
       MPI_Send(&pol7[0], 1, polynom_type, 4, tag7, MPI_COMM_WORLD);
       MPI_Send(&pol8[0], 1, polynom_type, 4, tag8, MPI_COMM_WORLD);

       
       
       MPI_Recv(&pol12[0], 1, polynom_type, 1, tag11, MPI_COMM_WORLD, &Status);
       
       printf("TAG 0, 11: ");
       for (int j = 0; j < 8 * size_p - 7; j++) {
           printf("%d ", pol12[j]);
       }
       printf("\n");
       

   }
   else if (ProcRank == 1) {
       printf("PROC: 1_\n");
       int* pol1 = new int[8 * size_p - 7];
       int* pol2 = new int[8 * size_p - 7];
       int* pol3 = new int[8 * size_p - 7];
       int* pol4 = new int[8 * size_p - 7];
       int* pol12 = new int[8 * size_p - 7];
       int* pol13 = new int[8 * size_p - 7];
       int* polres = new int[8 * size_p - 7];
       MPI_Recv(&pol1[0], 1, polynom_type, 0, tag1, MPI_COMM_WORLD, &Status);
       MPI_Recv(&pol2[0], 1, polynom_type, 0, tag2, MPI_COMM_WORLD, &Status);
       
       for (int j = 0; j < 8 * size_p - 7; j++) {
           pol12[j] = 0;
           pol13[j] = 0;
           polres[j] = 0;
       }
       
       for (int i = 0; i < size_p; i++) {
           for (int j = 0; j < size_p; j++) {
               pol12[i+j] += pol1[i] * pol2[j];
           }
       }

       printf("TAG 1,2: ");
       for (int j = 0; j < 2 * size_p - 1; j++) {
           printf("%d ", pol12[j]);
       }
       printf("\n");
       
       MPI_Recv(&pol3[0], 1, polynom_type, 2, tag21, MPI_COMM_WORLD, &Status);

       
       for (int i = 0; i < 2*size_p-1; i++) {
           for (int j = 0; j < 2*size_p-1; j++) {
               pol13[i+j] += pol3[i] * pol12[j];
           }
       }

       printf("TAG 1,21: ");
       for (int j = 0; j < 4 * size_p - 3; j++) {
           printf("%d ", pol13[j]);
       }
       printf("\n");
       
       MPI_Recv(&pol4[0], 1, polynom_type, 3, tag31, MPI_COMM_WORLD, &Status);
       
       for (int i = 0; i < 4*size_p-3; i++) {
           for (int j = 0; j < 4*size_p-3; j++) {
               polres[i+j] += pol4[i] * pol12[j];
           }
       }

       printf("TAG 1,31: ");
       for (int j = 0; j < 8 * size_p - 7; j++) {
           printf("%d ", polres[j]);
       }
       printf("\n");
       
       MPI_Send(&polres[0], 1, polynom_type, 0, tag11, MPI_COMM_WORLD);


       delete[] pol1;
       delete[] pol2;
       delete[] pol3;
       delete[] pol4;
       delete[] pol12;

   }
   else if (ProcRank == 2) {
       printf("PROC: 2_\n");
       int* pol1 = new int[8 * size_p - 7];
       int* pol2 = new int[8 * size_p - 7];
       int* pol12 = new int[8 * size_p - 7];
       for (int i = 0; i < 2*size_p-1; i++) {
           pol12[i] = 0;
       }
       MPI_Recv(&pol1[0], 1, polynom_type, 0, tag3, MPI_COMM_WORLD, &Status);
       MPI_Recv(&pol2[0], 1, polynom_type, 0, tag4, MPI_COMM_WORLD, &Status);
       
       for (int i = 0; i < size_p; i++) {
           for (int j = 0; j < size_p; j++) {
               pol12[i+j] += pol1[i] * pol2[j];
               
           }
       }

       printf("TAG 3,4: ");
       for (int j = 0; j < 2 * size_p - 1; j++) {
           printf("%d ", pol12[j]);
       }
       printf("\n");
       
       MPI_Send(&pol12[0], 1, polynom_type, 1, tag21, MPI_COMM_WORLD);
       delete[] pol1;
       delete[] pol2;
       delete[] pol12;

   }
   else if (ProcRank == 3) {
       printf("PROC: 3_\n");
       int* pol1 = new int[8 * size_p - 7];
       int* pol2 = new int[8 * size_p - 7];
       int* pol3 = new int[8 * size_p - 7];
       int* pol12 = new int[8 * size_p - 7];
       int* polres = new int[8 * size_p - 7];

       for (int i = 0; i < 8 * size_p - 7; i++) {
           pol12[i] = 0;
           polres[i] = 0;
       }
       MPI_Recv(&pol1[0], 1, polynom_type, 0, tag5, MPI_COMM_WORLD, &Status);
       MPI_Recv(&pol2[0], 1, polynom_type, 0, tag6, MPI_COMM_WORLD, &Status);
       
       for (int i = 0; i < size_p; i++) {
           for (int j = 0; j < size_p; j++) {
               pol12[i+j] += pol1[i] * pol2[j];
           }
       }

       printf("TAG 5,6: ");
       for (int j = 0; j < 2 * size_p - 1; j++) {
           printf("%d ", pol12[j]);
       }
       printf("\n");

       
       MPI_Recv(&pol3[0], 1, polynom_type, 4, tag41, MPI_COMM_WORLD, &Status);
       
       for (int i = 0; i < 2*size_p-1; i++) {
           for (int j = 0; j < 2*size_p-1; j++) {
               polres[i+j] += pol3[i] * pol12[j];
           }
       }

       printf("TAG 3, 41: ");
       for (int j = 0; j < 4 * size_p - 3; j++) {
           printf("%d ", polres[j]);
       }
       printf("\n");
       
       MPI_Send(&polres[0], 1, polynom_type, 1, tag31, MPI_COMM_WORLD);

       delete[] pol1;
       delete[] pol2;
       delete[] pol3;
       delete[] pol12;
       delete[] polres;
   }
   else if (ProcRank == 4) {
       printf("PROC: 4_\n");
       int* pol1 = new int[8*size_p-7];
       int* pol2 = new int[8 * size_p - 7];
       int* pol12 = new int[8 * size_p - 7];
       for (int i = 0; i < 8 * size_p - 7; i++) {
           pol12[i] = 0;
       }
       MPI_Recv(&pol1[0], 1, polynom_type, 0, tag7, MPI_COMM_WORLD, &Status);
       MPI_Recv(&pol2[0], 1, polynom_type, 0, tag8, MPI_COMM_WORLD, &Status);
       
       for (int i = 0; i < size_p; i++) {
           for (int j = 0; j < size_p; j++) {
               pol12[i+j] += pol1[i] * pol2[j];
           }
       }

       printf("TAG 7,8: ");
       for (int j = 0; j < 2 * size_p - 1; j++) {
           printf("%d ", pol12[j]);
       }
       printf("\n");
       
       MPI_Send(&pol12[0], 1, polynom_type, 3, tag41, MPI_COMM_WORLD);
       delete[] pol1;
       delete[] pol2;
       delete[] pol12;
   }
   
   MPI_Type_free(&polynom_type);
   MPI_Finalize();

   return 0;
}