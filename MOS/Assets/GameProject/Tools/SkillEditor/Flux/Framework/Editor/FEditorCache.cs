using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

using Flux;

namespace FluxEditor
{
	/**
	 * @brief Used to cache the editor classes that will be used to handle the individual elements
	 * (e.g. FTimelineEditor, FEventEditor).
	 */
	public class FEditorCache : ScriptableObject
	{
		// runtime dictionary used to hold the editor cache
		private Dictionary<int, FEditor> _editorHash;

		// used to save all the editor dictionary ids
		[SerializeField]
		private List<int> _editorHashKeys = new List<int>();

		// used to save all the editor dictionary values
		[SerializeField]
		private List<FEditor> _editorHashValues = new List<FEditor>();

		public void OnEnable()
		{
			hideFlags = HideFlags.DontSave;
		}

		public T GetEditor<T>( FObject obj ) where T : FEditor
		{
			if( _editorHash == null )
			{
				Refresh();
			}

			if( obj == null )
			{
				Debug.Log ("obj is null" );
			}

			if( _editorHash.ContainsKey( obj.GetInstanceID() ) )
			{
				return (T)_editorHash[obj.GetInstanceID()];
			}
			
			
			Type[] allTypes = typeof( FEditor ).Assembly.GetTypes();
			
			Type editorType = typeof( T );
			
			Type bestEditorType = editorType;
			
			Type objType = obj.GetType();
			
			Type closestObjType = objType;
			
			foreach( Type type in allTypes )
			{
				if( !type.IsSubclassOf( editorType ) )
					continue;
				
				object[] attributes = type.GetCustomAttributes( false );
				
				foreach( object o in attributes )
				{
					if( !(o is FEditorAttribute) )
						continue;
					
					FEditorAttribute editorAttribute = (FEditorAttribute)o;
					if( editorAttribute.type == objType )
					{
						bestEditorType = type;
						break;
					}
					
					if( editorAttribute.type.IsAssignableFrom( objType ) && editorAttribute.type.IsSubclassOf( closestObjType ) )
					{
						bestEditorType = type;
						closestObjType = editorAttribute.type;
					}
				}
			}
			T editor = (T)Editor.CreateInstance( bestEditorType );
			
			_editorHash.Add( obj.GetInstanceID(), editor );
			_editorHashKeys.Add( obj.GetInstanceID() );
			_editorHashValues.Add( editor );
	
			return editor;
		}

		public void Remove( FEditor editor )
		{
			if( _editorHash == null )
				Refresh();
			_editorHash.Remove( editor.Obj.GetInstanceID() );
			_editorHashKeys.Remove( editor.Obj.GetInstanceID() );
			_editorHashValues.Remove( editor );
		}

		public void Refresh()
		{
			if( _editorHash == null )
				_editorHash = new Dictionary<int, FEditor>();
			else
				_editorHash.Clear();

			for( int i = 0; i < _editorHashValues.Count; ++i )
			{
				FObject runtimeObj = _editorHashValues[i] == null ? null : _editorHashValues[i].Obj;
				if( _editorHashValues[i] == null ||  object.Equals(runtimeObj, null) )
				{
					_editorHashKeys.RemoveAt( i );
					_editorHashValues.RemoveAt( i );
					--i;
				}
				else
				{
					if( runtimeObj == null ) // unity null, so it has an instance ID
					{
						_editorHashValues[i].RefreshRuntimeObject();
					}
					_editorHash.Add( _editorHashKeys[i], _editorHashValues[i] );
				}
			}
		}

		public void Clear()
		{
			if( _editorHash == null )
				_editorHash = new Dictionary<int, FEditor>();
			else
				_editorHash.Clear();
			
			for( int i = 0; i < _editorHashValues.Count; ++i )
			{
				if( _editorHashValues[i] != null )
					Editor.DestroyImmediate( _editorHashValues[i] );
			}
			
			_editorHashKeys.Clear();
			_editorHashValues.Clear();
		}
	}
}
