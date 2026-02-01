# Hw1_QiLiu

https://github.com/Airinaleo/Hw1_QiLiu.git


Challenges and Solutions

1. Software & Build 
Developing on a Mac was difficult. Internet restrictions blocked Unity 6 downloads, and I had to manually fix missing SDK/JDK/NDK via Android Studio. After several attempts, the project was finally built. I built it twice.

2. Black Screen 
Fixed by enabling Meta Quest Support in the OpenXR Feature Group. Or the other method I tried. 

3. Controller 
The controller didn't work at first. I had to delete and recreate the XR Origin to get the headset to recognize the controllers correctly. 
I try to use InputActionReference in scripts, as the HW1 description rubric/table said, but it doesn't work in my project, so I give up using the reference, then I drag the path directly. Or prioritize input polling? I am not quite sure; it is a long journey to know the basic logic of unity.

4. Direction (X, Y, Z) 
The orientation was confusing. I added room_point and external_point as anchors. By aligning the blue Z-axis arrows, I fixed the player's facing direction.

5. Gravity Issues 
The player kept falling. I fixed this by setting the Tracking Mode to Floor and disabling Use Gravity in the Move Provider.

Summary
This was much harder than expected. Since I am remote, I couldn't visit TS135. However, I aim to do better in HW2. Thanks to Elmeri Uotila and Paula for your patience.


