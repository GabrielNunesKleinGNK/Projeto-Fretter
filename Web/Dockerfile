FROM node:14.17.0 AS build
WORKDIR /usr/src/app
COPY Web/Fretter.Web/package.json Web/Fretter.Web/package-lock.json ./
RUN npm install
COPY Web/Fretter.Web/ .
COPY Web/Fretter.Web/nginx.conf .
RUN npm run build
### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY --from=build /usr/src/app/dist /usr/share/nginx/html
COPY Web/Fretter.Web/nginx.conf /etc/nginx/conf.d/default.conf
