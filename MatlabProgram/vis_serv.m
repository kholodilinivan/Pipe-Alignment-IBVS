clc
clear al
Tc = transl(0.5, 0.5, 0)*trotx(0.3);
P = circle([0 0 3], 0.5, 'n', 10);
Tc = [0 0 0];
p = cam.project(P, 'pose', Tc);
pn = cam.normalized( p);
x = pn(1,:); y = pn(2,:);
a = [y.^2; -2*x.*y; 2*x; 2*y; ones(1,numcols(x))]';
b = -(x.^2)';
E = a\b;
plane = [0 0 1 -3]; 
J = cam.visjac_e(E, plane);
%%
cam = CentralCamera('default'); 
ibvs = IBVS_e(cam, 'example');
ibvs.run()