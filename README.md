# poolingSystem

This project was made with Unity 2019.4.8f1

To edit the max size of the pool change the poolSize variable on the PoolingSystem gameObject.
To edit the amount of objects spawned using the UI, change the initialSpawnValue on the ObjectSpawner gameObject.
To edit the area that the objects randomly move within edit the dimensions on the ObjectContainer gameObject.

To further optimize the project I would focus on multithreading the FindNearestNeighbour script with the Jobs system and BurstCompiler.
 
