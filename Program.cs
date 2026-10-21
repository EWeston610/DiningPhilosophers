using System;
using System.Threading;

class DiningPhilosophers
{
    static object[] forks = new object[5];
    static void Main()
    {
        for(int i = 0; i < 5; i++)
        {
            forks[i] = new object();
        }

        Thread[] professors = new Thread[5];

        for(int i = 0; i < 5; i++)
        {
            int id = i;
            professors[i] = new Thread(() => Dine(id));
            professors[i].Start();
        }
    }

    static void Dine(int id)
    {
        int leftFork = id;
        int rightFork = (id+1)%5;

        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"Professor {id} is resting/talking");
            Thread.Sleep(500);

            Console.WriteLine($"Professor {id} is waiting for LEFT fork {leftFork}...");

            lock (forks[leftFork])
            {
                Console.WriteLine($"Professor {id} grabbed LEFT fork {leftFork}");
                Thread.Sleep(100);

                Console.WriteLine($"Professor {id} is waiting for RIGHT fork {rightFork}...");

                lock (forks[rightFork])
                {
                    Console.WriteLine($"Professor {id} grabbed RIGHT fork {rightFork}");
                    Console.WriteLine($"Professor {id} is eating");
                    Thread.Sleep(500);
                }
                Console.WriteLine($"Professor {id} put down RIGHT fork {rightFork}");
            }
            Console.WriteLine($"Professor {id} put down LEFT fork {leftFork}");
        }
    }
}

//Deadlock because every professor has grabbed the fork on their left and is premanently blocked waiting for the fork on the right