#version 330 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

uniform float myOffset;

out vec3 ourColor; // output the fargment shader

void main() {
	gl_Position = vec4(aPos.x + myOffset, aPos.y, aPos.z, 1.0);
	ourColor = aColor;
};