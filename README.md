# Interactive-Unity-Tool
 An interactive Unity-based 3D tool that allows users to spawn, select, manipulate objects and navigate the scene camera using intuitive mouse and keyboard controls.
The project mimics basic Unity Scene View functionality and is intended for learning, prototyping, and interactive tool development.

# Script Overview
 # ObjectManager.cs
  Spawns primitive objects
  Handles object selection
  Updates object position and rotation via UI input fields

# CameraManager.cs
 Implements camera zoom, pan, and rotation
 Uses a pivot-based camera system
 Enables smooth scene navigation similar to Unity Scene View

 # Controls
  Select Object: Left Mouse Click on the Object.
  Rotate Camera: Hold Right Mouse Button + Move Mouse
  Pan Camera: Hold Middle Mouse Button + Move Mouse
  Zoom In/Out: Mouse Scroll Wheel
  Focus Camera on Selected Object: L key on Keyboard

  # Highlight
   Visual highlight for the selected object using LineWork free outline package from Unity Asset Store.
