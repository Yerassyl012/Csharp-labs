#include <iostream>
#include <mpi.h>
#include <stdio.h>
int main(int argc, char** argv)
{
	// Ваш код без использования MPI функций
	// Инициализируем библиотеку
	MPI_Init(&argc, &argv);

	//Ваш код для реализации обмена
	printf("Hello World!\n");
	MPI_Finalize();
	return 0;
}
