clc
clear all
close all

% Camera parameters
load('Omni_Calib_Results_9sim.mat'); % Calib parameters
ocam_model = calib_data.ocam_model; % Calib parameters

% mapping configuration
camX = 0; % Camera parameters
camY = 0; % Camera parameters
camZ = 0; % Camera parameters
lasX = 0; % Laser Plane parameters
lasY = 0; % Laser Plane parameters
las_dist = 666; % Laser Plane parameters

% Initialization Parameters
server_ip = '127.0.0.1';     % IP address of the Unity Server
server_port = 55001;         % Server Port of the Unity Sever
height = 1920; % image size
width = 1920; % image size

% The Client obj that is created is the connection to the server
Client = TCPInit(server_ip,server_port);

figure;
% SetLocation(Client,x,y,alpha)
image = GetImage(Client,height,width);
pause(0.15);
imshow(image);

%% Mapping
img = las_segm_red(image,248); % Laser Segmentation
%%
[x1,y1] = mapping(img,camX,camY,camZ,lasX,lasY,...
    las_dist,ocam_model); % mapping function
% Finally figure:
figure;
scatter(x1,y1,5,'filled'); % Laser intersections, first image
hold on;
% Reference circle
[xc,yc,R,a] = circfit(x1,y1);  
th = 0:pi/50:2*pi;
xunit = R * cos(th) + xc;
yunit = R * sin(th) + yc;
plot(xunit, yunit);
plot(xc, yc, 'o');
grid on;
axis equal

%Close Gracefully
fprintf(1,"Disconnected from server\n");
R