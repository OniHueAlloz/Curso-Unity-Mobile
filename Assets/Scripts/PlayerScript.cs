using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerScript : MonoBehaviour
{
    [Header("Controles Animacao")]
    public Animator PlayerAnimator;
    private Rigidbody2D PlayerRigidbody;
    public Transform DetectorSuperficie;
    public bool SobreSuperficie;
    public LayerMask Plataformas;
    public int idAnimation;
    public bool Jump;
    private bool BloqueiaJump;
    public bool Attack1;
    public bool Attack2;
    public bool Hit;
    public float ForcaPulo;
    public float Velocidade;
    private float h, v;

    [Header("Controle Zoom")]
    public float ZoomMaximo;
    public float ZoomMinimo;
    public float ZoomPadrao;
    public float PassoZoom;
    private float ZoomAtual;
    public float TempoEntreMaximoMinimo;
    private float FPS;
    public CinemachineVirtualCamera VirtualCamera;

    [Header("Head-Up Display")]
    public Sprite[] DPAD_Sprites;
    public SpriteRenderer DPAD;
    public Transform Delimitador;
    public float DPAD_x, DPAD_y, Delimitador_x;
    private Touch Toque;
    public Transform Cursor;
    public float RaioMomentaneo, RaioMinimo, RaioMaximo, x, y;
    private int idFinger;
    private bool JumpBotao, Attack1Botao, Attack2Botao, MenuBotao;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        BloqueiaJump = false;

        DPAD_x = Camera.main.WorldToScreenPoint(DPAD.transform.position).x;
        DPAD_y = Camera.main.WorldToScreenPoint(DPAD.transform.position).y;
        Delimitador_x = Camera.main.WorldToScreenPoint(Delimitador.position).x;
        RaioMaximo = Delimitador_x - DPAD_x;
        RaioMinimo = 0.15f * RaioMaximo;
    }

    // Update is called once per frame
    void Update()
    {        
        DetectarTeclado();

        DetectarTouch();

        FPS = 1 / Time.deltaTime;
        // print(Time.time + " FPS = " + FPS);

        PassoZoom = ((ZoomMinimo - ZoomMaximo) / FPS) / TempoEntreMaximoMinimo;


        SobreSuperficie = Physics2D.OverlapCircle(new Vector2(DetectorSuperficie.position.x, DetectorSuperficie.position.y), 0.1f, Plataformas);
        
        if (SobreSuperficie)
        {
            BloqueiaJump = false;
        }

        if (v > 0)
        {
            DPAD.sprite = DPAD_Sprites[1];
            ZoomIn();
        }
        else if (v < 0)
        {
            DPAD.sprite = DPAD_Sprites[3]; 
            ZoomOut();
        }

        if (h != 0)
        {
            if (h > 0)
            {
                DPAD.sprite = DPAD_Sprites[2];
            }
            else
            {
                DPAD.sprite = DPAD_Sprites[4];
            }
            
            if (SobreSuperficie)
            {
                idAnimation = 2;
            }
            PlayerRigidbody.transform.localScale = new Vector3(h, 1, 1);
        }
        else if (v == 0)
        {
            DPAD.sprite = DPAD_Sprites[0];
            if (SobreSuperficie)
            {
                idAnimation = 0;
            }
        }

        if ((Jump ||JumpBotao) && !BloqueiaJump)
        {
            JumpBotao = false;
            if (!SobreSuperficie)
            {
                BloqueiaJump = true;
            }
            PlayerAnimator.SetTrigger("Jump");
            PlayerRigidbody.AddForce(new Vector3(0, ForcaPulo, 0));
        }
        if (Attack1 || Attack1Botao)
        {
            Attack1Botao = false;
            PlayerAnimator.SetTrigger("Attack1");
        }
        if (Attack2 || Attack2Botao)
        {
            Attack2Botao = false;
            PlayerAnimator.SetTrigger("Attack2");
        }
        if (Hit)
        {
            PlayerAnimator.SetTrigger("Hit");
        }

        if (PlayerRigidbody.velocity.y < 0)
        {
            idAnimation = 3;
        }

        PlayerAnimator.SetInteger("idAnimation", idAnimation);

        PlayerRigidbody.velocity = new Vector3(h * Velocidade, PlayerRigidbody.velocity.y, 0);
    }

    private void DetectarTeclado()
    {
        Jump = Input.GetButtonDown("Jump");
        Attack1 = Input.GetButtonDown("Fire2");
        Attack2 = Input.GetButtonDown("Fire1");
        Hit = Input.GetButtonDown("Cancel");
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        //print(Time.time + " v = " + v + " h = " + h);
    }

    public void ZoomIn()
    {
        ZoomAtual = VirtualCamera.m_Lens.OrthographicSize - PassoZoom;
        if (ZoomAtual < ZoomMaximo)
        {
            ZoomAtual = ZoomMaximo;
        }
        VirtualCamera.m_Lens.OrthographicSize = ZoomAtual;
    }

    public void ZoomOut()
    {
        ZoomAtual = VirtualCamera.m_Lens.OrthographicSize + PassoZoom;
        if (ZoomAtual > ZoomMinimo)
        {
            ZoomAtual = ZoomMinimo;
        }
        VirtualCamera.m_Lens.OrthographicSize = ZoomAtual;
    }

    private void DetectarTouch()
    {
        // print(Time.time + " Dedos sobre a tela = " + Input.touchCount);
        if (Input.touchCount > 0) 
        {
            for (int i = 0; i < Input.touchCount; i++)
            { 
                Toque = Input.GetTouch(i);
                Cursor.position = Camera.main.ScreenToWorldPoint(new Vector3(Toque.position.x, Toque.position.y, 1));

                if (Toque.phase == TouchPhase.Stationary || Toque.phase == TouchPhase.Moved || Toque.phase == TouchPhase.Ended)
                {
                    idFinger = i;

                    x = Toque.position.x - DPAD_x;
                    y = Toque.position.y - DPAD_y;

                    RaioMomentaneo = Mathf.Sqrt(x * x + y * y);

                    if (RaioMomentaneo < RaioMaximo)
                    {
                        //print(Time.time + " D-PAD Ativado");

                        if (RaioMomentaneo < RaioMinimo)
                        {
                            print(Time.time + " Centro do D-PAD: Ponto Morto");
                            h = 0;
                            v = 0;
                        }
                        else
                        {
                            if (Mathf.Abs(x)  > Mathf.Abs(y))
                            {
                                if (x > 0)
                                {
                                    h = 1;
                                }
                                else
                                {
                                    h = -1;
                                }
                            }
                            else
                            {
                                if (y > 0)
                                {
                                    v = 1;
                                }
                                else
                                {
                                    v = -1;
                                }
                            }
                        }
                    }
                    else if (Toque.phase == TouchPhase.Ended && idFinger == i) 
                    {
                        //print(Time.time + " Fora do D-PAD");
                        h = 0;
                        v = 0;
                    }
                }
            
            }
        }
    }

    public void BotaoA()
    {
        JumpBotao = true;
    }

    public void BotaoB()
    {
        Attack2Botao = true;
    }

    public void BotaoX()
    {
        Attack1Botao = true;
    }
    public void BotaoY()
    {
        MenuBotao = true;
    }

}
