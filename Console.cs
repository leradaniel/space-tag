// Console
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
	public delegate void TemplateMethod();

	public KeyCode consoleKey = KeyCode.F8;

	public KeyCode consoleKeyJoy = KeyCode.Joystick1Button6;

	public GameObject consola;

	public Text backgroundText;

	public InputField inputText;

	public float downSpeed;

	public float topPosition = 450f;

	public bool consolePause;

	private bool activateActions;

	private bool consoleMoving;

	public Dictionary<string, CommandData> allCommands = new Dictionary<string, CommandData>();

	public bool damageUP;

	private void Start()
	{
		consola.transform.localPosition = new Vector3(0f, topPosition, 0f);
		Clear();
		consola.SetActive(value: false);
		RegisterCommand("help", Help, "Recibe información sobre los comandos.");
		RegisterCommand("clear", Clear, "Limpia el texto de la consola.");
		RegisterCommand("vidasmax", VidasMax, "Aumenta la cantidad de vidas al máximo.");
		RegisterCommand("saludmax", FullHealth, "Llena al máximo la salud de las naves con vida.");
		RegisterCommand("inmunidad", Inmunidad, "Otorga inmunidad hasta que se cambie de nave.");
		RegisterCommand("velocidad", Velocidad, "Las naves tienen velocidad de movimiento máxima.");
		RegisterCommand("cadencia", Cadencia, "Las naves disparan el doble de rápido.");
		RegisterCommand("regenvelocidad", RegeneracionVel, "La nave guardada regenera el doble de rápido.");
		RegisterCommand("regenpoder", RegeneracionPod, "La nave guardada regenera el triple de vida.");
		RegisterCommand("destruccion", DamageUpgrade, "El daño de las naves se incrementa muchísimo.");
	}

	public void RegisterCommand(string commandName, TemplateMethod commandCommand, string description)
	{
		CommandData commandData = new CommandData();
		commandData.name = commandName.ToLower();
		commandData.description = description;
		commandData.command = commandCommand;
		allCommands.Add(commandData.name, commandData);
	}

	private void RegeneracionPod()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameObject.Find("GameManager").GetComponent<GameManager>().regenerationPercentage *= 3;
	}

	private void RegeneracionVel()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameObject.Find("GameManager").GetComponent<GameManager>().regenerationInterval /= 2f;
	}

	private void Cadencia()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[0].bulletCooldown /= 2f;
		GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[1].bulletCooldown /= 2f;
	}

	private void Velocidad()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[0].shipSpeed = 60f;
		GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[1].shipSpeed = 60f;
	}

	private void Inmunidad()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameObject.Find("GameManager").GetComponent<GameManager>().ship.GetComponent<Collider>().enabled = false;
		GameObject.Find("GameManager").GetComponent<GameManager>().altship.GetComponent<Collider>().enabled = false;
	}

	private void FullHealth()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[0].healthPointsNow = GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[0].healthPointsMax;
		GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[1].healthPointsNow = GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[1].healthPointsMax;
	}

	public void DamageUpgrade()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		damageUP = true;
	}

	public void VidasMax()
	{
		GetComponent<AudioSource>().pitch = 3f;
		GetComponent<AudioSource>().Play();
		GameManager.lifes = 4;
		GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLifes();
	}

	public void Clear()
	{
		backgroundText.text = string.Empty;
		backgroundText.text += "Usá el comando HELP para recibir ayuda.\n";
	}

	public void Help()
	{
		foreach (KeyValuePair<string, CommandData> allCommand in allCommands)
		{
			if (allCommand.Value.name != "help")
			{
				Write(allCommand.Value.name + ": " + allCommand.Value.description);
			}
		}
	}

	private void Update()
	{
		if (!GameManager.pause || consolePause)
		{
			ToggleConsole();
			ManageText();
			ActivateConsole();
		}
	}

	private void ToggleConsole()
	{
		if (Input.GetKeyDown(consoleKey) || Input.GetKeyDown(consoleKeyJoy))
		{
			if (!consola.activeSelf)
			{
				consola.SetActive(value: true);
				consolePause = true;
				GameManager.pause = true;
				consoleMoving = true;
				GetComponent<AudioSource>().pitch = 1.5f;
				GetComponent<AudioSource>().Play();
			}
			else if (activateActions && !consoleMoving)
			{
				GetComponent<AudioSource>().pitch = 1.3f;
				GetComponent<AudioSource>().Play();
				consoleMoving = true;
			}
		}
	}

	private void ActivateConsole()
	{
		if (consola.activeSelf)
		{
			if (consoleMoving && !activateActions && consolePause && consola.transform.localPosition.y > 0f)
			{
				consola.transform.localPosition -= new Vector3(0f, downSpeed * Time.deltaTime, 0f);
			}
			else if (consoleMoving && !activateActions && consola.transform.localPosition.y <= 0f)
			{
				consola.transform.localPosition = new Vector3(0f, 0f, 0f);
				inputText.ActivateInputField();
				inputText.Select();
				activateActions = true;
				consoleMoving = false;
			}
			else if (consoleMoving && activateActions && consola.transform.localPosition.y < topPosition)
			{
				consola.transform.localPosition += new Vector3(0f, downSpeed * Time.deltaTime, 0f);
			}
			else if (consoleMoving && activateActions && consola.transform.localPosition.y >= topPosition)
			{
				consola.transform.localPosition = new Vector3(0f, topPosition, 0f);
				activateActions = false;
				consola.SetActive(value: false);
				consolePause = false;
				GameManager.pause = false;
				consoleMoving = false;
			}
		}
	}

	private void ManageText()
	{
		if (!consola.activeSelf || !activateActions)
		{
			return;
		}
		inputText.text = inputText.text.ToLower();
		if (!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			return;
		}
		if (allCommands.ContainsKey(inputText.text))
		{
			try
			{
				allCommands[inputText.text].command();
			}
			catch (NullReferenceException ex)
			{
				Write("NULL ERROR: " + ex.Message + "\n\n" + ex.StackTrace);
			}
			catch (Exception ex2)
			{
				Write("ERROR: " + ex2.Message + "\n\n" + ex2.StackTrace);
				throw;
			}
		}
		else
		{
			Write("\"" + inputText.text + "\" is a null command. Try using 'help'.");
		}
		inputText.text = string.Empty;
	}

	public void Write(string txt)
	{
		Text text = backgroundText;
		text.text = text.text + txt + "\n";
	}
}
