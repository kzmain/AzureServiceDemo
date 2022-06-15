MODE="JJCR_IC"
MODE="JJCR_CN"
MODE="AZCR_CN"

# Set AKS information
if [ $MODE = "JJCR_IC" ]; then
        AKS_RG="AZR-ZAJ-DSGWTS-Production"
        AKS_DK="kai-test-aks-disk-instance"
        AKS_CL="azr-zaj-dsgwts-aks-test"
elif [ $MODE = "AZCR_CN" ]; then
        AKS_RG="Kai-Test-AKS"
        AKS_DK="kai-test-aks-disk-instance" # not use in dynamic
        AKS_CL="Kai-Test-AKS-Instance"
else # $MODE = "JJCR_CN"
        AKS_RG="Kai-Test-AKS"
        AKS_DK="kai-test-aks-disk-instance" # not use in dynamic
        AKS_CL="Kai-Test-AKS-Instance"
fi

# Set Docker information
if [ $MODE = "JJCR_IC" ]; then
        DOCKER_IMAGE_UER_NAME="kzhang62"
        DOCKER_IMAGE_UER_KEY=$KAI_JNJ_PWD
        DOCKER_IMAGE_URL="jcfh-docker.artifactrepo.jnj.com"
elif [ $MODE = "AZCR_CN" ]; then
        DOCKER_IMAGE_UER_NAME="kaiaks"
        DOCKER_IMAGE_UER_KEY='B+BsKEphIiyegxMqEp4FUWy6ZWBNMt4Z'
        DOCKER_IMAGE_URL="kaiaks.azurecr.cn"
else # $MODE = "JJCR_CN"
        DOCKER_IMAGE_UER_NAME="kzhang62"
        DOCKER_IMAGE_UER_KEY=$KAI_JNJ_PWD
        DOCKER_IMAGE_URL="jcfh-docker.artifactrepo.jnj.com"
fi

# Set AKS pull docker image credential name
K8SCREDENTIALNAME="aksregistrykey"

az cloud set --name AzureChinaCloud
az login
# Set AKS subscription
if [ $MODE = "JJCR_IC" ]; then
        az account set --subscription 242d709d-84e3-4836-bbf0-d02a6a7601e7
elif [ $MODE = "AZCR_CN" ]; then
        az account set --subscription 44fc1485-c550-4413-9349-7ef90e0f59b1
else # $MODE = "JJCR_CN"
        az account set --subscription 44fc1485-c550-4413-9349-7ef90e0f59b1
fi

# Get k8s cluster secret
az aks get-credentials --resource-group $AKS_RG --name $AKS_CL -force
# Save docker repo secret to the k8s cluster
kubectl delete secret $K8SCREDENTIALNAME
kubectl create secret docker-registry $K8SCREDENTIALNAME --docker-server=$DOCKER_IMAGE_URL --docker-username=$DOCKER_IMAGE_UER_NAME --docker-password=$DOCKER_IMAGE_UER_KEY --docker-email=KZhang62@ITS.JNJ.com

# Delete the old pods from the AKS
if [ $MODE = "JJCR_IC" ]; then
        kubectl delete -f AKS_JJCR_IC/MVC1_PVC.yaml
        kubectl delete -f AKS_JJCR_IC/MVC2_PVC.yaml
        kubectl delete -f AKS_JJCR/Grpc1.yaml
        kubectl delete -f AKS_JJCR/Grpc2.yaml
        kubectl delete -f AKS_JJCR/Grpc3.yaml
elif [ $MODE = "AZCR_CN" ]; then
        kubectl delete -f AKS_AZCR_CN/MVC1_PVC.yaml
        kubectl delete -f AKS_AZCR_CN/MVC2_PVC.yaml
        kubectl delete -f AKS_AZCR_CN/Grpc1.yaml
        kubectl delete -f AKS_AZCR_CN/Grpc2.yaml
        kubectl delete -f AKS_AZCR_CN/Grpc3.yaml
else # $MODE = "JJCR_CN"
        kubectl delete -f AKS_JJCR_CN/MVC1_PVC.yaml
        kubectl delete -f AKS_JJCR_CN/MVC2_PVC.yaml
        kubectl delete -f AKS_JJCR/Grpc1.yaml
        kubectl delete -f AKS_JJCR/Grpc2.yaml
        kubectl delete -f AKS_JJCR/Grpc3.yaml
fi
kubectl delete -f AKS_Disk.yaml

# Deploy the new pods to the AKS
kubectl apply -f AKS_Disk.yaml
if [ $MODE = "JJCR_IC" ]; then
        kubectl apply -f AKS_JJCR/Grpc1.yaml
        kubectl apply -f AKS_JJCR/Grpc2.yaml
        kubectl apply -f AKS_JJCR/Grpc3.yaml
        kubectl apply -f AKS_JJCR_IC/MVC1.yaml
        kubectl apply -f AKS_JJCR_IC/MVC2.yaml
elif [ $MODE = "AZCR_CN" ]; then
        kubectl apply -f AKS_AZCR_CN/Grpc1.yaml
        kubectl apply -f AKS_AZCR_CN/Grpc2.yaml
        kubectl apply -f AKS_AZCR_CN/Grpc3.yaml
        kubectl apply -f AKS_AZCR_CN/MVC1.yaml
        kubectl apply -f AKS_AZCR_CN/MVC2.yaml
else # $MODE = "JJCR_CN"
        kubectl apply -f AKS_JJCR/Grpc1.yaml
        kubectl apply -f AKS_JJCR/Grpc2.yaml
        kubectl apply -f AKS_JJCR/Grpc3.yaml
        kubectl apply -f AKS_JJCR_CN/MVC1.yaml
        kubectl apply -f AKS_JJCR_CN/MVC2.yaml
fi

# Upload the pfx file to the AKS PVC
# kubectl cp https/saqa_jnj_com_cn.pfx <podname>/https
# kubectl exec -it <podname> -- /bin/sh
if [ $MODE = "JJCR_IC" ]; then
        kubectl delete -f AKS_JJCR_IC/MVC1.yaml
        kubectl delete -f AKS_JJCR_IC/MVC2.yaml
        kubectl apply -f  AKS_JJCR_IC/MVC1_PVC.yaml
        kubectl apply -f  AKS_JJCR_IC/MVC2_PVC.yaml
elif [ $MODE = "AZCR_CN" ]; then
        kubectl delete -f AKS_AZCR_CN/MVC1.yaml
        kubectl delete -f AKS_AZCR_CN/MVC2.yaml
        kubectl apply -f  AKS_AZCR_CN/MVC1_PVC.yaml
        kubectl apply -f  AKS_AZCR_CN/MVC2_PVC.yaml
else # $MODE = "JJCR_CN"
        kubectl delete -f AKS_JJCR_CN/MVC1.yaml
        kubectl delete -f AKS_JJCR_CN/MVC2.yaml
        kubectl apply -f  AKS_JJCR_CN/MVC1_PVC.yaml
        kubectl apply -f  AKS_JJCR_CN/MVC2_PVC.yaml
fi