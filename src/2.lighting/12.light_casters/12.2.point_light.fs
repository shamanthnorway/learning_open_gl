#version 330 core
out vec4 FragColor;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float shininess;
};

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constant;
    float linear;
    float quadratic;
};

in vec3 Normal;  
in vec3 FragPos;  
in vec2 TextCoords;

uniform vec3 objectColor;
uniform vec3 viewPos;

uniform Material material;
uniform Light light;

void main()
{
    float distance = length(light.position - FragPos);
    float attenuation = 1.0/(light.constant + (light.linear * distance) + (light.quadratic * distance * distance));
    
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * vec3(texture(material.diffuse, TextCoords)));
    diffuse *= attenuation;

    // ambient
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TextCoords));
    ambient *= attenuation;

    //specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TextCoords));
    specular *= attenuation;
            
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
} 