docker buildx build --platform linux/amd64 -t azureservicedemo_mvc1  -f MVC1/Dockerfile  . --no-cache
docker buildx build --platform linux/amd64 -t azureservicedemo_mvc2  -f MVC2/Dockerfile  . --no-cache
docker buildx build --platform linux/amd64 -t azureservicedemo_grpc1 -f Grpc1/Dockerfile . --no-cache
docker buildx build --platform linux/amd64 -t azureservicedemo_grpc2 -f Grpc2/Dockerfile . --no-cache
docker buildx build --platform linux/amd64 -t azureservicedemo_grpc3 -f Grpc3/Dockerfile . --no-cache

docker image tag azureservicedemo_mvc1  kaiaks.azurecr.cn/azureservicedemo_mvc1:x64_from_mac
docker image tag azureservicedemo_mvc2  kaiaks.azurecr.cn/azureservicedemo_mvc2:x64_from_mac
docker image tag azureservicedemo_grpc1 kaiaks.azurecr.cn/azureservicedemo_grpc1:x64_from_mac
docker image tag azureservicedemo_grpc2 kaiaks.azurecr.cn/azureservicedemo_grpc2:x64_from_mac
docker image tag azureservicedemo_grpc3 kaiaks.azurecr.cn/azureservicedemo_grpc3:x64_from_mac

docker image push kaiaks.azurecr.cn/azureservicedemo_mvc1:x64_from_mac
docker image push kaiaks.azurecr.cn/azureservicedemo_mvc2:x64_from_mac
docker image push kaiaks.azurecr.cn/azureservicedemo_grpc1:x64_from_mac
docker image push kaiaks.azurecr.cn/azureservicedemo_grpc2:x64_from_mac
docker image push kaiaks.azurecr.cn/azureservicedemo_grpc3:x64_from_mac

docker image tag azureservicedemo_mvc1  jcfh-docker.artifactrepo.jnj.com/azureservicedemo_mvc1:x64_from_mac
docker image tag azureservicedemo_mvc2  jcfh-docker.artifactrepo.jnj.com/azureservicedemo_mvc2:x64_from_mac
docker image tag azureservicedemo_grpc1 jcfh-docker.artifactrepo.jnj.com/azureservicedemo_grpc1:x64_from_mac
docker image tag azureservicedemo_grpc2 jcfh-docker.artifactrepo.jnj.com/azureservicedemo_grpc2:x64_from_mac
docker image tag azureservicedemo_grpc3 jcfh-docker.artifactrepo.jnj.com/azureservicedemo_grpc3:x64_from_mac

docker image push jcfh-docker.artifactrepo.jnj.com/azureservicedemo_mvc1:x64_from_mac
docker image push jcfh-docker.artifactrepo.jnj.com/azureservicedemo_mvc2:x64_from_mac
docker image push jcfh-docker.artifactrepo.jnj.com/azureservicedemo_grpc1:x64_from_mac
docker image push jcfh-docker.artifactrepo.jnj.com/azureservicedemo_grpc2:x64_from_mac
docker image push jcfh-docker.artifactrepo.jnj.com/azureservicedemo_grpc3:x64_from_mac
