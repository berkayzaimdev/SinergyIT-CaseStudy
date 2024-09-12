pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Restore') {
            steps {
                script {
                    dir('src') {
                        sh 'dotnet restore'
                    }
                }
            }
        }
        stage('Build') {
            steps {
                script {
                    dir('src') {
                        sh 'dotnet build --configuration Release'
                    }
                }
            }
        }
        stage('SonarQube Analysis') {
            steps {
                script {
                    withSonarQubeEnv('SonarQube') {
                        dir('src') {
                            sh 'dotnet sonarscanner begin /k:"testproj" /d:sonar.login="sqp_7c9495e4e1a96b07156b51c1923573da8bd142db"'
                            sh 'dotnet build'
                            sh 'dotnet sonarscanner end /d:sonar.login="sqp_7c9495e4e1a96b07156b51c1923573da8bd142db"'
                        }
                    }
                }
            }
        }
        stage('Quality Gate') {
            steps {
                waitForQualityGate()
            }
        }
    }
}
