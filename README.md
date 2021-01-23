# ThroughTheRings
Simple asteroid navigation game.

Board your space capsule and explore the asteroid-packed ring system of a nearby planet. Your goal is to identify specific asteroids suitable for resource mining and transmit your findings to headquarters. Communications services while inside the asteroid field are expected to be unreliable, so you will have to make your transmission after exiting the ring system. 

This is a simple game aimed at demonstrating the following topics:

a) structural composition concepts that involve very large objects (asteroids, planets, etc.) 

b) how Unity technologies such as ECS, URP and ShaderGraph can be used to create a fully-featured commercial entertainment environment.


First Phase:

The goal is to evaluate the spatial properties of the scene and the technique used to facilitate long distances. 

Second Phase:

Create a simple ringed planet system that consists of a central shpere (Planet) and various disc-like layers of token cubes scattered around it (Asteroids).
The end result should posess a few basic properties of a standard ring system, such as random initial rotation, size, self-rotation speed and distance from the Planet and its neighbouring Asteroids.

Third Phase:

The Asteroids should be able to implement collisions and gravity forces, affecting their motion. Collisions should also result in Asteroid fragmentation (debris). Those Physics should also affect the Capsule. 

Fourth Phase:

By now the Scene must be filled with tens of thousands of moving GameObjects, making rendering very difficult. Refactor the whole project using ECS. Add features, controls and artifacts inside the Capsule. 

Fifth Phase:

Try to make the Scene as compelling as possible. See how much load it can handle.


