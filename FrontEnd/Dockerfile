###  Erasmo Cardoso

# Usar a imagem oficial do Nginx
FROM nginx:alpine

RUN rm /usr/share/nginx/html/*

COPY index.html /usr/share/nginx/html/

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
