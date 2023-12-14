FROM node:18 as builder
WORKDIR /usr/src/app

COPY package.json package-lock.json index.js ./
RUN npm install

FROM node:18-alpine

#RUN apt-get update && apt-get install -y wget

ENV DOCKERIZE_VERSION v0.6.1
RUN wget https://github.com/jwilder/dockerize/releases/download/$DOCKERIZE_VERSION/dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && tar -C /usr/local/bin -xzvf dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz \
    && rm dockerize-linux-amd64-$DOCKERIZE_VERSION.tar.gz

# Copie apenas os arquivos necessários da fase de construção anterior
COPY --from=builder /usr/src/app .

EXPOSE 3000

CMD ["node","index.js"]
