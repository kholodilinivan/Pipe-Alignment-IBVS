function MotionVer(TCP_Handle,speed)
int = 6; % 0-img, 1-transform, 2-get position, 3-mode, 4-laser, 5-set position,
% 6 - motion vertical, 7 - motion horizontal
write(TCP_Handle,single([int,speed]));