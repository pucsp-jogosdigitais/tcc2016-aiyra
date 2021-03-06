using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class SimpleModel : MonoBehaviour 
{
    #region Attributes

    public TextAsset mocFile ;
    public TextAsset motionfile;
    public Texture2D[] textureFiles ;
	
	private Live2DModelUnity live2DModel;
    private Live2DMotion motion;
    private MotionQueueManager motionmgr;
    private Matrix4x4 live2DCanvasPos;

    public Animator anim;
    public int mtnnum = 0;
    private bool changeflg = false;

    #endregion

    #region Methods

    void Start ()
    {
        if (live2DModel != null) return;
        Live2D.init();
        //captura e armazena o animator do ator na variavel anim
        anim = GetComponent<Animator>();

        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
        for (int i = 0; i < textureFiles.Length; i++)
        {
            live2DModel.setTexture(i, textureFiles[i]);
        }

        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        //instancia a varieval motionmgr para o uso
        motionmgr = new MotionQueueManager();
    }
	
    void Update()
    {
        if (live2DModel == null) return;
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        double t = (UtSystem.getUserTimeMSec() / 1000.0) * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_ANGLE_X", (float)(30 * Math.Sin(t / 3.0)));
        
        live2DModel.update();
    }
	
	void OnRenderObject()
	{
        //deixa claro para a maquina de se model moc for nulo o metodo retorna void e evita possivel erros por falta de recursos
        if (live2DModel == null) return;
        //seta a matrix do modelo para a posição em relação ao mundo e vetoriza o modelo no mundo respeitando sua posição
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        //checa se o aplicativo não esta rodando para então executar as funções update e desenhar do live2d
        if(!Application.isPlaying)
        {
            live2DModel.update();
            live2DModel.draw();
            //depois de executar as linhas anteriores retorna terminando seu funcionamente para então poder voltar a checagem
            return;
        }

        //checa se a animação atual é diferente da atual animaçao do animator do ator
        if(mtnnum != anim.GetInteger("Motion"))
        {
            //Passa o valor da animação atual do animator para o mtnnum variavel que indica que animação deve ser rodada
            mtnnum = anim.GetInteger("Motion");
            //carrega o motionfile em formato de bytes para ser carregada e armazenada na variavel motion
            motion = Live2DMotion.loadMotion(motionfile.bytes);
            //Muda a changeflg para verdadeira
            changeflg = true;
        }

        //checa se a animação terminou ou se teve alteração de animação para então iniciar a animação nova ou reiniciar a animação
        if(motionmgr.isFinished() || changeflg == true)
        {
            //Da start para a animação
            motionmgr.startMotion(motion);
            //da falso para a variavel que indica que mudou de animação
            changeflg = false;
        }

        //da update para os parametros de live2d
        motionmgr.updateParam(live2DModel);

        //Update e desenho do modelo do live2d
        live2DModel.update();
        live2DModel.draw();

        live2DModel.draw();
    }

    #endregion
}