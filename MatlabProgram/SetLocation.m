function SetLocation(TCP_Handle,x,y,alpha)
int = 5; % 0-img, 1-transform, 2-get position, 3-mode, 4-laser, 5-set position
write(TCP_Handle,single([int,x,y,alpha]));