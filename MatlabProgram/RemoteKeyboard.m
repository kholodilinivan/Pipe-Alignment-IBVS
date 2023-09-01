% Discrete movements
speed = 5;
rotspeed = 2;
w = 1;
s = 0;
a = 1;
d = 0;

height = 320;
width = 320;

laser = 0; % 0 - turn off, 1 - turn on
LaserActive(Client,laser);
pause(0.15);
mode = 0; % 0 - simple img, 1 - segmented img
SetMode(Client,mode);
pause(0.15);
figure;
for count = 1:10
    RobotControl(Client,w,s,a,d,speed,rotspeed);
    pause(0.15);
    image = GetImage(Client,height,width);
    pause(0.15);
    [x,y,rot] = GetLocation(Client);
    pause(0.15);
    imshow(image);
end

%Close Gracefully
fprintf(1,"Disconnected from server\n");