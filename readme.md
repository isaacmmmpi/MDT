### MDT （Mocap Data Transformation）

#### Introduction

This is a program developed based on C# to convert the neural network model export result of video-to-pose3d into mocap data in C3d format acceptable to motionbuilder and other software.

![](http://106.15.93.194/assets/MDT.png)

#### Requirement

The program contains calls to Python scripts, so your computer must be equipped with a python environment.

- Python 3.6

#### Usage

I make a [video](https://www.bilibili.com/video/av81506027) on bilibili to show you how to use it.

However, you can still learn about it by following these steps:

1. This program must be based on the [video-to-pose3d](https://github.com/zh-plus/video-to-pose3D#coming-soon) project,it's an in-depth learning model of human posture capture improved by Chinese programmers on the basis of Facebook.

2. After running the model, a file in. NPY format will be generated. Import the file into the program, and then you can observe the action of the character in the preview window.
3. Then click **Create C3d File** button, you can generate action capture file in C3d format.

Tips:

1. You can change the zoom size of the character by adjusting the value of **Proportion**.
2. Do not change **Catch Points** if not necessary.
3. You can adjust the animation play speed by sliding **Display Speed**.

#### Acknowledgement

Thanks for the C3d library file provided by [**mayswind**](https://github.com/mayswind).(I wrote to him personally, and he helped me solve the problem enthusiastically.Truly thanks a lot)

Thanks for the video-to-pose3d model developed by Facebook and the improvement of zh-plus.

#### Agreement

This class library follows the open source protocol and is not allowed to be used commercially.

#### Donate

If you want to sponsor me, you can scan the QR code below. Thank you for your support anyway.

![](http://106.15.93.194/donate/donate.png)

