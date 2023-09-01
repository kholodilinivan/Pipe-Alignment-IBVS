function image = GetImage(TCP_Handle,height,width)

write(TCP_Handle,single([0,height,width])); % 0-img, 1-transform, 2-both img&transform
%Get image data
data = read(TCP_Handle,height*width*3);

%Organizing image data
imagetmp = uint8(zeros(width,height,3));
image = uint8(zeros(height,width,3));
r = uint8(zeros(height,width));
g = uint8(zeros(height,width));
b = uint8(zeros(height,width));

r = data(1:3:width*height*3);
g = data(2:3:width*height*3);
b = data(3:3:width*height*3);
for y = 1:height
    for x = 1:width
        imagetmp(x,y,1) = r((y-1)*width+x);
        imagetmp(x,y,2) = g((y-1)*width+x);
        imagetmp(x,y,3) = b((y-1)*width+x);
    end
end

image = uint8(imrotate(imagetmp,90));