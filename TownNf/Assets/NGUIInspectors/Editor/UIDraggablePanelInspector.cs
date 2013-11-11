using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System;
using Object = UnityEngine.Object;
using InspectorPlusVar = UIDraggablePanelInspector.InspectorPlusVar;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIDraggablePanel))]
public class UIDraggablePanelInspector : Editor {
    public class InspectorPlusVar
    {
        public enum LimitType { None, Max, Min, Range };
        public enum VectorDrawType { None, Direction, Point, PositionHandle, Scale, Rotation };
        public LimitType limitType = LimitType.None;
        public float min = -0.0f;
        public float max = -0.0f;
        public bool progressBar;
        public int iMin = -0;
        public int iMax = -0;
        public bool active = true;
        public string type;
        public string name;
        public string dispName;
        public VectorDrawType vectorDrawType;
        public bool relative = false;
        public bool scale = false;
        public float space = 0.0f;
        public bool[] labelEnabled = new bool[4];
        public string[] label = new string[4];
        public bool[] labelBold = new bool[4];
        public bool[] labelItalic = new bool[4];
        public int[] labelAlign = new int[4];
        public bool[] buttonEnabled = new bool[16];
        public string[] buttonText = new string[16];
        public string[] buttonCallback = new string[16];
        public bool[] buttonCondense = new bool[4];

        public int numSpace = 0;
        public string classType;
        public Vector3 offset = new Vector3(0.5f, 0.5f);
        public bool QuaternionHandle;
	    public bool canWrite = true;
	    public string tooltip;
	    public bool hasTooltip = false;
        public bool toggleStart = false;
        public int toggleSize = 0;
        public int toggleLevel = 0;
        public bool largeTexture;
        public float textureSize;

		public string textFieldDefault;
		public bool textArea;

    public InspectorPlusVar(LimitType _limitType, float _min, float _max, bool _progressBar, int _iMin, int _iMax, bool _active, string _type, string _name, string _dispName,
                        VectorDrawType _vectorDrawType, bool _relative, bool _scale, float _space, bool[] _labelEnabled, string[] _label, bool[] _labelBold, bool[] _labelItalic, int[] _labelAlign, bool[] _buttonEnabled, string[] _buttonText,
                        string[] _buttonCallback, bool[] buttonCondense, int _numSpace, string _classType, Vector3 _offset, bool _QuaternionHandle, bool _canWrite, string _tooltip, bool _hasTooltip,
                        bool _toggleStart, int _toggleSize, int _toggleLevel, bool _largeTexture, float _textureSize, string _textFieldDefault, bool _textArea)
    {
        limitType = _limitType;
        min = _min;
        max = _max;
        progressBar = _progressBar;
        iMax = _iMax;
        iMin = _iMin;
        active = _active;
        type = _type;
        name = _name;
        dispName = _dispName;
        vectorDrawType = _vectorDrawType;
        relative = _relative;
        scale = _scale;
        space = _space;
        labelEnabled = _labelEnabled;
        label = _label;
        labelBold = _labelBold;
        labelItalic = _labelItalic;
        labelAlign = _labelAlign;
        buttonEnabled = _buttonEnabled;
        buttonText = _buttonText;
        buttonCallback = _buttonCallback;
        numSpace = _numSpace;
        classType = _classType;
        offset = _offset;
        QuaternionHandle = _QuaternionHandle;
        canWrite = _canWrite;
        tooltip = _tooltip;
        hasTooltip = _hasTooltip;
        toggleStart = _toggleStart;
        toggleSize = _toggleSize;
        toggleLevel = _toggleLevel;
        largeTexture = _largeTexture;
        textureSize = _textureSize;
		textFieldDefault = _textFieldDefault;
		textArea = _textArea;
    }
    }	
    SerializedObject so;
	SerializedProperty[] properties;
	new string name;
    string dispName;
	Rect tooltipRect;	
	List<InspectorPlusVar> vars;
	void RefreshVars(){for (int i = 0; i < vars.Count; i += 1) properties[i] = so.FindProperty (vars[i].name);}
	void OnEnable ()
	{
        vars = new List<InspectorPlusVar>();
        so = serializedObject;
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"Boolean","restrictWithinPanel","Restrict Within Panel",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Whether the dragging will be restricted to be within the parent panel's bounds.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"Boolean","disableDragIfFits","Disable Drag If Fits",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Whether dragging will be disabled if the contents fit.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"DragEffect","dragEffect","Drag Effect",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Effect to apply when dragging.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"Vector3","scale","Scale",InspectorPlusVar.VectorDrawType.Scale,true,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(1f,0f,0f),false,true,"Scale value applied to the drag delta. Set X or Y to 0 to disallow dragging in that direction.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"Single","scrollWheelFactor","Scroll Wheel Factor",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Effect the scroll wheel will have on the momentum.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.Range,0,125,false,0,0,true,"Single","momentumAmount","Momentum Amount",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"How much momentum gets applied when the press is released after dragging.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"Vector2","relativePositionOnReset","Relative Position On Reset",InspectorPlusVar.VectorDrawType.PositionHandle,true,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Starting position of the clipped area. (0, 0) means top-left corner, (1, 1) means bottom-right.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"Boolean","repositionClipping","Reposition Clipping",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Whether the position will be reset to the 'startingDragAmount'. Inspector-only value.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"UIScrollBar","horizontalScrollBar","Horizontal Scroll Bar",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Horizontal scrollbar used for visualization.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"UIScrollBar","verticalScrollBar","Vertical Scroll Bar",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Vertical scrollbar used for visualization.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"ShowCondition","showScrollBars","Show Scroll Bars",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Condition that must be met for the scroll bars to become visible.",true,false,0,0,false,70,"",false));
        vars.Add(new InspectorPlusVar(InspectorPlusVar.LimitType.None,0,0,false,0,0,true,"OnDragFinished","onDragFinished","On Drag Finished",InspectorPlusVar.VectorDrawType.None,false,false,0,new System.Boolean[]{false,false,false,false},new System.String[]{"","","",""},new System.Boolean[]{false,false,false,false},new System.Boolean[]{false,false,false,false},new System.Int32[]{0,0,0,0},new System.Boolean[]{false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false},new System.String[]{"","","","","","","","","","","","","","","",""},new System.String[]{"","","","","","","","","","","","","","","",""},new System.Boolean[]{false,false,false,false},0,"UIDraggablePanel",new Vector3(0.5f,0.5f,0f),false,true,"Event callback to trigger when the drag process finished. Can be used for additional effects, such as centering on some object.",true,false,0,0,false,70,"",false));	
		int count = vars.Count;
		properties = new SerializedProperty[count];
	}
    
	void ProgressBar (float value, string label)
	{
		GUILayout.Space (3.0f);
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		GUILayout.Space (3.0f);
	}
	void Vector2Field(SerializedProperty sp)
	{
        EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);
		EditorGUI.BeginChangeCheck ();
		var newValue = EditorGUILayout.Vector2Field (dispName, sp.vector2Value);

		if (EditorGUI.EndChangeCheck ())
			sp.vector2Value = newValue;
		
		EditorGUI.EndProperty ();
	}
	void FloatField(SerializedProperty sp, InspectorPlusVar v)
	{
		if (v.limitType == InspectorPlusVar.LimitType.Min && !sp.hasMultipleDifferentValues)
			sp.floatValue = Mathf.Max (v.min, sp.floatValue);
		else if (v.limitType == InspectorPlusVar.LimitType.Max && !sp.hasMultipleDifferentValues)
			sp.floatValue = Mathf.Min (v.max, sp.floatValue);
		
		if (v.limitType == InspectorPlusVar.LimitType.Range) {
			if (!v.progressBar)
				EditorGUILayout.Slider (sp, v.min, v.max);
			else {
				if (!sp.hasMultipleDifferentValues) {
					sp.floatValue = Mathf.Clamp (sp.floatValue, v.min, v.max);
					ProgressBar ((sp.floatValue - v.min) / v.max, dispName);
				} else
					ProgressBar ((sp.floatValue - v.min) / v.max, dispName);
			}
		}
        else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
	}
    int BoolField(SerializedProperty sp, InspectorPlusVar v)
    {
        if (v.toggleStart)
        {
            EditorGUI.BeginProperty(new Rect(0.0f, 0.0f, 0.0f, 0.0f), new GUIContent(), sp);

            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUILayout.Toggle(dispName, sp.boolValue);
            
            if (EditorGUI.EndChangeCheck())
                sp.boolValue = newValue;
            
            EditorGUI.EndProperty();

            if (!sp.boolValue)
                return v.toggleSize;
        }
        else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));

        return 0;
    }
	void PropertyField (SerializedProperty sp, string name)
	{
		if (sp.hasChildren) {
            GUILayout.BeginVertical();
			while (true) {
				if (sp.propertyPath != name && !sp.propertyPath.StartsWith (name + "."))
					break;

				EditorGUI.indentLevel = sp.depth;
                bool child = false;

                if (sp.depth == 0)
                    child = EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
                else
                    child = EditorGUILayout.PropertyField(sp);

				if (!sp.NextVisible (child))
					break;
			}
            EditorGUI.indentLevel = 0;
            GUILayout.EndVertical();
		} else EditorGUILayout.PropertyField(sp, new GUIContent(dispName));
	}
	public override void OnInspectorGUI ()
	{	
		so.Update ();
		RefreshVars();
		
		EditorGUIUtility.LookLikeControls (135.0f, 50.0f);

		for (int i = 0; i < properties.Length; i += 1) 
		{
			InspectorPlusVar v = vars[i];
			
			if (v.active && properties[i] != null) 
			{
				SerializedProperty sp = properties [i];string s = v.type;
							 bool skip = false;
				name = v.name;
                dispName = v.dispName;

				GUI.enabled = v.canWrite;

                GUILayout.BeginHorizontal();

                if (v.toggleLevel != 0)
                   GUILayout.Space(v.toggleLevel * 10.0f);
                
                if (s == typeof(Vector2).Name){
                    Vector2Field(sp);
                    skip = true;
                }
                if (s == typeof(float).Name){
                    FloatField(sp, v);
                    skip = true;
                }
                if (s == typeof(bool).Name){
                    i += BoolField(sp, v);
                    skip = true;
                }
                if (!skip)
                    PropertyField(sp, name);
                GUILayout.EndHorizontal();
                GUI.enabled = true;
				if (v.hasTooltip)
				{
	                Rect last = GUILayoutUtility.GetLastRect();
	                GUI.Label(last, new GUIContent("", v.tooltip));

                    GUIStyle style = new GUIStyle();
                    style.fixedWidth = 250.0f;
                    style.wordWrap = true;

					Vector2 size = new GUIStyle().CalcSize(new GUIContent(GUI.tooltip));
					tooltipRect = new Rect(Event.current.mousePosition.x + 4.0f, Event.current.mousePosition.y + 12.0f, 28.0f + size.x, 9.0f + size.y);

                    if (tooltipRect.width > 250.0f)
                    {
                        float delt = (tooltipRect.width - 250.0f);
                        tooltipRect.width -= delt;
                        tooltipRect.height += size.y * Mathf.CeilToInt(delt / 250.0f);
                    }
				}
			}
		}
        so.ApplyModifiedProperties (); 
		if (!string.IsNullOrEmpty(GUI.tooltip))
        {
            GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            GUI.Box(tooltipRect, new GUIContent());
			EditorGUI.HelpBox(tooltipRect, GUI.tooltip, MessageType.Info);
			Repaint();
		}
		GUI.tooltip = "";
        //NOTE NOTE NOTE: WATERMARK HERE
        //You are free to remove this
        //START REMOVE HERE
        GUILayout.BeginHorizontal();
        GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        GUILayout.FlexibleSpace();
        GUILayout.Label("Created with");
        GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
        if (GUILayout.Button("Inspector++"))
            Application.OpenURL("http://forum.unity3d.com/threads/136727-Inspector-Meh-to-WOW-inspectors");
        GUI.color = new Color(1.0f, 1.0f, 1.0f);
		GUILayout.EndHorizontal();
        //END REMOVE HERE
    }
	void VectorScene(InspectorPlusVar v, string s, Transform t)
	{
		Vector3 val;
		
		if (s == typeof(Vector3).Name)
			val = (Vector3)GetTargetField(name);
		else 
			val = (Vector3)((Vector2)GetTargetField(name));
		
		Vector3 newVal = Vector3.zero;
		Vector3 curVal = Vector3.zero;
		bool setVal = false;
		bool relative = v.relative;
		bool scale = v.scale;

		switch (v.vectorDrawType) {
		case InspectorPlusVar.VectorDrawType.Direction:
			curVal = relative ? val:val - t.position;
            float size = scale ? Mathf.Min(2.0f, Mathf.Sqrt(curVal.magnitude) / 2.0f) : 1.0f;
            size *= HandleUtility.GetHandleSize(t.position);
			Handles.ArrowCap (0, t.position, curVal != Vector3.zero ? Quaternion.LookRotation (val.normalized) : Quaternion.identity, size);
			break;

		case InspectorPlusVar.VectorDrawType.Point:
			curVal = relative ? val:t.position + val;
			Handles.SphereCap (0, curVal, Quaternion.identity, 0.1f);
			break;

		case InspectorPlusVar.VectorDrawType.PositionHandle:
			curVal = relative ? t.position + val:val;
			setVal = true;
			newVal = Handles.PositionHandle (curVal, Quaternion.identity) - (relative ? t.position : Vector3.zero);
			break;

		case InspectorPlusVar.VectorDrawType.Scale:
			setVal = true;
            curVal = relative ? t.localScale + val :val;
            newVal = Handles.ScaleHandle(curVal, t.position + v.offset, t.rotation, HandleUtility.GetHandleSize(t.position + v.offset)) - (relative ? t.localScale : Vector3.zero);
			break;
			
		case InspectorPlusVar.VectorDrawType.Rotation:
			setVal = true;
            curVal = relative ? val + t.rotation.eulerAngles : val;
			newVal = Handles.RotationHandle(Quaternion.Euler(curVal), t.position + v.offset).eulerAngles - (relative?t.rotation.eulerAngles:Vector3.zero);
			break;
		}
	
		if (setVal)
		{
			object newObjectVal = newVal;
			
			if (s==typeof(Vector2).Name)
				newObjectVal = (Vector2)newVal;
			else if (s == typeof(Quaternion).Name)
				newObjectVal = Quaternion.Euler(newVal);
						
			SetTargetField(name, newObjectVal);
		}
	}
	object GetTargetField(string name){return target.GetType ().GetField (name).GetValue (target);}
	void SetTargetField(string name, object value){target.GetType ().GetField (name).SetValue (target, value);}
	//some magic to draw the handles
	public void OnSceneGUI ()
	{
		Transform t = ((MonoBehaviour)target).transform;
		
		foreach (InspectorPlusVar v in vars) {
			if (!v.active)
				continue;

			string s = v.type;
			name = v.name;
			if (s == typeof(Vector3).Name || s == typeof(Vector2).Name) 
				VectorScene(v, s, t);
	    }
    }
}