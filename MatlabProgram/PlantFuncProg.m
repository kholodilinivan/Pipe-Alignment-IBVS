clc
clear all
close all

% Initialization Parameters
server_ip = '127.0.0.1';     % IP address of the Unity Server
server_port = 55001;         % Server Port of the Unity Sever
% The Client obj that is created is the connection to the server
Client = TCPInit(server_ip,server_port);


for i = 1:10
    speed = -2;
    MotionVer(Client,speed);
    pause(0.15);
    speed = 10;
    MotionHor(Client,speed);
    pause(0.15);
end

%Close Gracefully
fprintf(1,"Disconnected from server\n");
%%
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
height = 480; % image size
width = 480; % image size

% The Client obj that is created is the connection to the server
Client = TCPInit(server_ip,server_port);

% Set Position
x = 0; % position along x-axis
y = 0; % position along y-axis
alpha = 0; % orientation
figure;
% SetLocation(Client,x,y,alpha)
image = GetImage(Client,height,width);
pause(0.15);
imshow(image);














%%
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
%
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





%% Mapping
clc
clear all
close all

% Camera parameters
load('Omni_Calib_Results_9sim.mat'); % Calib parameters
ocam_model = calib_data.ocam_model; % Calib parameters

% mapping configuration
camX = -2; % Camera parameters
camY = -4; % Camera parameters
camZ = 0; % Camera parameters
lasX = 6; % Laser Plane parameters
% lasX = 0; % Laser Plane parameters
lasY = 0; % Laser Plane parameters
las_dist = 470.6; % Laser Plane parameters

image = imread('aa2.jpg');
img = las_segm_red(image,158); % Laser Segmentation
%
[x1,y1] = mapping(img,camX,camY,camZ,lasX,lasY,...
    las_dist,ocam_model); % mapping function
% Finally figure:
figure;
scatter(x1,y1,5,'filled','b'); % Laser intersections, first image
hold on;
% Reference circle
[xc,yc,R,a] = circfit(x1,y1);  
th = 0:pi/50:2*pi;
xunit = R * cos(th) + xc;
yunit = R * sin(th) + yc;
% plot(xunit, yunit);
plot(xc, yc, 'o','MarkerEdgeColor','b');



% mapping configuration
camX = 0; % Camera parameters
camY = 0; % Camera parameters
camZ = 0; % Camera parameters
lasX = 0; % Laser Plane parameters
lasY = 0; % Laser Plane parameters
las_dist = 430.3; % Laser Plane parameters
[x1,y1] = mapping(img,camX,camY,camZ,lasX,lasY,...
    las_dist,ocam_model); % mapping function
scatter(x1,y1,5,'filled', 'm'); % Laser intersections, first image
[xc,yc,R1,a] = circfit(x1,y1);  
th = 0:pi/50:2*pi;
xunit = R1 * cos(th) + xc;
yunit = R1 * sin(th) + yc;
% plot(xunit, yunit);
plot(xc, yc, 'o', 'MarkerEdgeColor','m');


grid on;
axis equal