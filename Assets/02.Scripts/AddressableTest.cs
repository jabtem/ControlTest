using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor;

public class AddressableTest : MonoBehaviour
{
    // Start is called before the first frame update 
    public string groupName = "Test";

    public string path = "Assets/03.Prefabs/AddTest.prefab";

    string customAddress = "AddTest";
    AddressableAssetGroup group;

    private void Awake()
    {
        //group = settings.FindGroup(groupName);
        //if (!group)
        //{
        //    group = settings.CreateGroup(groupName, false, false, true, null, typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
        //    //group = setting.CreateGroup(groupName, false, false, false, new List<AddressableAssetGroupSchema> { setting.DefaultGroup.Schemas[0] });
        //}
        //string guid = AssetDatabase.AssetPathToGUID(path);
        //AddressableAssetEntry e = settings.CreateOrMoveEntry(guid, group);
        //e.address = customAddress;
        //var entry = new List<AddressableAssetEntry> { e };
        //settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);

        //AssetDatabase.SaveAssets();
    }
    void Start()
    {


        //BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
        //if(schema)
        //{
        //    schema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteLoadPath);
        //    schema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteBuildPath);
        //}


        ////group.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, false, true);
        //settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupSchemaModified, schema, true);

        //settings.SetDirty(AddressableAssetSettings.ModificationEvent.BuildSettingsChanged, settings, true);

        //AssetDatabase.SaveAssets();

    }

    [ContextMenu("Add")]
    public void Add()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        group = settings.FindGroup(groupName);
        if (!group)
        {
            group = settings.CreateGroup(groupName, false, false, true, null, typeof(BundledAssetGroupSchema), typeof(ContentUpdateGroupSchema));
            //group = setting.CreateGroup(groupName, false, false, false, new List<AddressableAssetGroupSchema> { setting.DefaultGroup.Schemas[0] });
        }
        string guid = AssetDatabase.AssetPathToGUID(path);
        AddressableAssetEntry e = settings.CreateOrMoveEntry(guid, group);
        e.address = customAddress;
        var entry = new List<AddressableAssetEntry> { e };
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);

        BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();

        schema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
        schema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupSchemaModified, schema, true);
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BuildSettingsChanged, settings, true);





        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
    [ContextMenu("Set Remote")]
    public void Set_Remote()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        group = settings.FindGroup(groupName);
        BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
        Debug.Log(schema);


        schema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteLoadPath);
        schema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kRemoteBuildPath);

        EditorUtility.SetDirty(schema);

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupSchemaModified, schema, true);

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BuildSettingsChanged, settings, true);





        AssetDatabase.SaveAssets();
    }

    [ContextMenu("Set Local")]
    public void Set_Local()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        group = settings.FindGroup(groupName);
        BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
        Debug.Log(schema);

        schema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
        schema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.GroupSchemaModified, schema, true);

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.BuildSettingsChanged, settings, true);





        AssetDatabase.SaveAssets();
    }
}
