# TODO

- [ ] remove blender materials from geometry
- [x] ensure UV on the start
- [x] fix multiple input controls
- [ ] asteroids
  - [x] basic spawn (one at a time)
  - [x] basic material
  - [x] basic movement
  - [x] basic rotation
  - [x] accell after rocket plane
  - [ ] collision
  - [ ] multiple speeds
  - [ ] algorithm to ensure possible to beat asteroid spawning
  - [x] interesting material
- [ ] wobble per damage
- [ ] pee bar
- [ ] Fidel's commentary UI
- [ ] star particle as 2D billboards
- [ ] star model with rounded corners (for stars hanging from the ceiling)
- [ ] flat shading on all polygons
- [ ] validation
  - [ ] camera should be pointing into positive z, from negative z without any angle

Option B: The URP Cel Shader (Best Visuals)

If you want the meteor to react to your game's directional light (e.g., the sun):

    Open Unity Shader Graph.

    Take the Normal Vector of the rock and get the Dot Product against the Main Light Direction. (This calculates how much the rock is facing the sun).

    Plug the result into a Step node. A Step node acts like a mathematical axe: instead of a smooth gradient from light to dark, it forces the light to be either 100% on or 100% off.

    Multiply this stepped result by your rock color (e.g., Purple or Gray).

    Bonus: Add an "Outline" by passing the material through a second pass that pushes the vertices outward slightly and colors them black (the "Inverted Hull" technique).

Summary of the "Toon Workflow" Upgrade:

    Mesh: Import a flat-shaded Icosphere prefab (~40 vertices).

    Deformation: You can still use your Perlin noise code to deform the prefab's vertices on startup! Because it has fewer vertices, the noise will create massive, chunky rock shapes instead of tiny wrinkles.

    Material: Use a URP Shader Graph with a Step node to create a hard shadow line, giving it that classic cartoon aesthetic.
