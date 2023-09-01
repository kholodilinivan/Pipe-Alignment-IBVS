%% Vertical direction
clc
clear all
close all
tic

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

% Set Position
% figure;
image = GetImage(Client,height,width);
pause(0.15);
% imshow(image);

% Mapping
img = las_segm_blue(image,-250); % Laser Segmentation
%
[x,y] = mapping(img,camX,camY,camZ,lasX,lasY,...
    las_dist,ocam_model); % mapping function
% Finally figure:
figure;
scatter(x,y,5,'filled'); % Laser intersections, first image
hold on;
% Reference circle
[xc,yc,R,a] = circfit(x,y);  
th = 0:pi/50:2*pi;
xunit = R * cos(th) + xc;
yunit = R * sin(th) + yc;
plot(xunit, yunit);
plot(xc, yc, 'o');
grid on;
axis equal

% PID control
setpoint = 0;        % desired output, or reference point

Kp = -100.0;        % proportional term Kp
Ki = -1;            % Integral term Ki
Kd = -1;             % derivative term Kd

if (sign(Kp)~=sign(Ki) | sign(Ki)~=sign(Kd))
    warning('All three gains should have the same sign for stability.');
end

dt = 0.01;          % sampling time
Time = 1;          % total simulation time in seconds
n = round(Time/dt); % number of samples

% pre-assign all the arrays to optimize simulation time
Prop(1:n+1) = 0; Der(1:n+1) = 0; Int_x(1:n+1) = 0; I(1:n+1) = 0;
u(1:n+1) = 0;
Output(1:n+1) = 0;
Error_x(1:n+1) = 0;

% Initial conditions
x1(1:n+1) = xc;
x1_dot(1:n+1) = xc;

xc_(1:n+1) = xc;
yc_(1:n+1) = yc;

% figure;
n
for i = 1:n
    % Control of the horizontal direction
    Error_x(i+1) = setpoint - x1(i); % error entering the PID controller
    Prop(i+1) = Error_x(i+1);% error of proportional term
    Der(i+1)  = (Error_x(i+1) - Error_x(i))/dt; % derivative of the error
    Int_x(i+1)  = Error_x(i+1)*dt; % integration of the error
    I_x(i+1)  = sum(Int_x); % the sum of the integration of the error    
    u(i+1)  = Kp*Prop(i) + Ki*I_x(i+1)+ Kd*Der(i); % the three PID terms    
    % You can replace the following lines with your system/hardware/model
    MotionHor(Client,u(i)/10000);
    pause(0.15);
    image = GetImage(Client,height,width);
    pause(0.15);
    % imshow(image);
    % Mapping
    img = las_segm_blue(image,-250); % Laser Segmentation
    %
    [x,y] = mapping(img,camX,camY,camZ,lasX,lasY,...
        las_dist,ocam_model); % mapping function
    % scatter(x,y,5,'filled'); % Laser intersections, first image
    % Reference circle
    [xc,yc,R,a] = circfit(x,y);  
    th = 0:pi/50:2*pi;
    xunit = R * cos(th) + xc;
    yunit = R * sin(th) + yc;
    % plot(xunit, yunit);
    xc_(i+1) = xc;
    yc_(i+1) = yc;
    plot(xc_, yc_, '-', 'color','b');
    hold on;
    grid on;
    axis equal
    x1(i+1) = xc; 
    i
end

tsim = toc % simulation time

% plot results
figure
T = 0:dt:Time;
Reference = setpoint*ones(1,i+1);
plot(T/10,Reference,'r-',T/10,x1,'b-','MarkerSize',2)
xlabel('Time [s]')
ylabel('Feedback/setpoint [mm]')
legend('Setpoint','Feedback')
set(gcf,'color','w')
%% Vertical direction
clc
clear all
close all
tic

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

image = GetImage(Client,height,width);
pause(0.15);
% imshow(image);

% Mapping
img = las_segm_blue(image,-250); % Laser Segmentation
%
[x,y] = mapping(img,camX,camY,camZ,lasX,lasY,...
    las_dist,ocam_model); % mapping function
% Finally figure:
figure;
scatter(x,y,5,'filled'); % Laser intersections, first image
hold on;
% Reference circle
[xc,yc,R,a] = circfit(x,y);  
th = 0:pi/50:2*pi;
xunit = R * cos(th) + xc;
yunit = R * sin(th) + yc;
plot(xunit, yunit);
plot(xc, yc, 'o');
grid on;
axis equal

% PID control
setpoint = 0;        % desired output, or reference point

Kp = -100.0;        % proportional term Kp
Ki = -1;            % Integral term Ki
Kd = -1;             % derivative term Kd

if (sign(Kp)~=sign(Ki) | sign(Ki)~=sign(Kd))
    warning('All three gains should have the same sign for stability.');
end

dt = 0.01;          % sampling time
Time = 1;          % total simulation time in seconds
n = round(Time/dt); % number of samples

% pre-assign all the arrays to optimize simulation time
Prop(1:n+1) = 0; Der(1:n+1) = 0; Int_x(1:n+1) = 0; I(1:n+1) = 0;
v(1:n+1) = 0;
Output(1:n+1) = 0;
Error_y(1:n+1) = 0;

% Initial conditions

y1(1:n+1) = yc;
y1_dot(1:n+1) = yc;
% figure;
n
for i = 1:n
    % Control of the vertical direction
    Error_y(i+1) = setpoint - y1(i); % error entering the PID controller   
    Prop(i+1) = Error_y(i+1);% error of proportional term
    Der(i+1)  = (Error_y(i+1) - Error_y(i))/dt; % derivative of the error
    Int_x(i+1)  = Error_y(i+1)*dt; % integration of the error
    I_y(i+1)    = sum(Int_x); % the sum of the integration of the error   
    v(i+1)  = Kp*Prop(i) + Ki*I_y(i+1)+ Kd*Der(i); % the three PID terms    
    % You can replace the following lines with your system/hardware/model
    MotionVer(Client,v(i)/10000);
    pause(0.15);
    image = GetImage(Client,height,width);
    pause(0.15);
    % imshow(image);
    % Mapping
    img = las_segm_blue(image,-250); % Laser Segmentation
    %
    [x,y] = mapping(img,camX,camY,camZ,lasX,lasY,...
        las_dist,ocam_model); % mapping function
    % scatter(x,y,5,'filled'); % Laser intersections, first image
    hold on;
    pause(0.15);
    % Reference circle
    [xc,yc,R,a] = circfit(x,y);  
    th = 0:pi/50:2*pi;
    xunit = R * cos(th) + xc;
    yunit = R * sin(th) + yc;
    % plot(xunit, yunit);
    plot(xc, yc, 'o');
    grid on;
    axis equal
    y1(i+1) = yc; 
    i
end

tsim = toc % simulation time

% plot results
figure
T = 0:dt:Time;
Reference = setpoint*ones(1,i+1);
plot(T/10,Reference,'r-',T/10,y1,'b-','MarkerSize',2)
xlabel('Time [s]')
ylabel('Feedback/setpoint [mm]')
legend('Setpoint','Feedback')
set(gcf,'color','w')
%% Both directions
clc
clear all
close all
tic

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

SetLocation(Client,300,260,90);
pause(0.15);
%
image = GetImage(Client,height,width);
pause(0.15);
% imshow(image);

% Mapping
img = las_segm_blue(image,-250); % Laser Segmentation
%
[x,y] = mapping(img,camX,camY,camZ,lasX,lasY,...
    las_dist,ocam_model); % mapping function
% Finally figure:
figure;
scatter(x,y,5,'filled'); % Laser intersections, first image
hold on;
% Reference circle
[xc,yc,R,a] = circfit(x,y);  
th = 0:pi/50:2*pi;
xunit = R * cos(th) + xc;
yunit = R * sin(th) + yc;
plot(xunit, yunit);
% plot(xc, yc, 'o', 'color','r');
grid on;
axis equal

% PID control
setpoint = 0;        % desired output, or reference point

Kp = -100.0;        % proportional term Kp
Ki = -1;            % Integral term Ki
Kd = -1;             % derivative term Kd

if (sign(Kp)~=sign(Ki) | sign(Ki)~=sign(Kd))
    warning('All three gains should have the same sign for stability.');
end

dt = 0.01;          % sampling time
Time = 1;          % total simulation time in seconds
n = round(Time/dt); % number of samples

% pre-assign all the arrays to optimize simulation time
Prop_x(1:n+1) = 0; Der_x(1:n+1) = 0; Int_x(1:n+1) = 0;
Prop_y(1:n+1) = 0; Der_y(1:n+1) = 0; Int_y(1:n+1) = 0;
I_x(1:n+1) = 0; I_y(1:n+1) = 0;
u(1:n+1) = 0;
v(1:n+1) = 0;
Output(1:n+1) = 0;
Error_x(1:n+1) = 0;
Error_y(1:n+1) = 0;

% Initial conditions
x1(1:n+1) = xc;
x1_dot(1:n+1) = xc;

y1(1:n+1) = yc;
y1_dot(1:n+1) = yc;

xc_(1) = xc;
yc_(1) = yc;
% figure;
n
for i = 1:n
    % Control of the horizontal direction
    Error_x(i+1) = setpoint - x1(i); % error entering the PID controller
    Prop_x(i+1) = Error_x(i+1);% error of proportional term
    Der_x(i+1)  = (Error_x(i+1) - Error_x(i))/dt; % derivative of the error
    Int_x(i+1)  = Error_x(i+1)*dt; % integration of the error
    I_x(i+1)    = sum(Int_x); % the sum of the integration of the error    
    u(i+1)  = Kp*Prop_x(i) + Ki*I_x(i+1)+ Kd*Der_x(i); % the three PID terms    
    % You can replace the following lines with your system/hardware/model
    MotionHor(Client,u(i)/10000);
    pause(0.43);
        
    
    
    % Control of the vertical direction
    Error_y(i+1) = setpoint - y1(i); % error entering the PID controller   
    Prop_y(i+1) = Error_y(i+1);% error of proportional term
    Der_y(i+1)  = (Error_y(i+1) - Error_y(i))/dt; % derivative of the error
    Int_y(i+1)  = Error_y(i+1)*dt; % integration of the error
    I_y(i+1)    = sum(Int_y); % the sum of the integration of the error   
    v(i+1)  = Kp*Prop_y(i) + Ki*I_y(i+1)+ Kd*Der_y(i); % the three PID terms    
    % You can replace the following lines with your system/hardware/model
    MotionVer(Client,v(i)/10000);
    pause(0.43);
    image = GetImage(Client,height,width);
    pause(0.43);
    % imshow(image);
    % Mapping
    img = las_segm_blue(image,-250); % Laser Segmentation
    %
    [x,y] = mapping(img,camX,camY,camZ,lasX,lasY,...
        las_dist,ocam_model); % mapping function
    % scatter(x,y,5,'filled'); % Laser intersections, first image
    hold on;
    % Reference circle
    [xc,yc,R,a] = circfit(x,y);  
    th = 0:pi/50:2*pi;
    xunit = R * cos(th) + xc;
    yunit = R * sin(th) + yc;
    % plot(xunit, yunit);
    xc_(i+1) = xc;
    yc_(i+1) = yc;
    plot(xc_, yc_, '.-', 'color','b');
    grid on;
    axis equal
    x1(i+1) = xc; 
    y1(i+1) = yc; 
    i
    if (i==50) 
        SetLocation(Client,-200,160,90);
        pause(0.43);
    end
end

tsim = toc % simulation time

%% plot results
figure
T = 0:dt:Time;
Reference = setpoint*ones(1,i+1);
plot(T,Reference,'r-',T,x1,'b-','MarkerSize',2)
xlabel('Time [s]')
ylabel('Feedback/setpoint [mm]')
legend('Setpoint','Feedback')
set(gcf,'color','w')

figure
T = 0:dt:Time;
Reference = setpoint*ones(1,i+1);
plot(T,Reference,'r-',T,y1,'b-','MarkerSize',2)
xlabel('Time [s]')
ylabel('Feedback/setpoint [mm]')
legend('Setpoint','Feedback')
set(gcf,'color','w')

figure
plot(xc_, yc_, '.-', 'color','b');
hold on
plot([0 0],'ro','MarkerSize',7);
xlabel('Horizontal feedback [mm]')
ylabel('Vertical feedback [mm]')
legend('Feedback','Setpoint')
set(gcf,'color','w')
axis equal