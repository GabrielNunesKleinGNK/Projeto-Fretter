FROM node:14.16.1 AS build
WORKDIR /usr/src/app
ENV NODE_OPTIONS="--max-old-space-size=16384"
COPY Web/Fretter.Web/package.json Web/Fretter.Web/package-lock.json ./
RUN npm cache clean --force
RUN npm install
COPY Web/Fretter.Web/ .
COPY Web/Fretter.Web/nginx.conf .
RUN npm run build
### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY --from=build /usr/src/app/dist /usr/share/nginx/html
COPY Web/Fretter.Web/nginx.conf /etc/nginx/conf.d/default.conf