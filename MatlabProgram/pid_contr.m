clear all
clc

num = 1;
denom = [1 3 1];

Gp = tf(num, denom);
H = 1;

Kp = 24;
Ki = 1;
Kd = 8;

Gc = pid(Kp,Ki,Kd);
Mc = feedback(Gc*Gp, H);
step(Mc);
grid on