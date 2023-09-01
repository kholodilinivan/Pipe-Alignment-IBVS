function RobotControl(TCP_Handle,w,s,a,d,speed,rotspeed)
int = 1; % 0-img, 1-transform, 2-get position, 3-mode, 4-laser, 5-set position
write(TCP_Handle,single([int,w,s,a,d,speed,rotspeed]));