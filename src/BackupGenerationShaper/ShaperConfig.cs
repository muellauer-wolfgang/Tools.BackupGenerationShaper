﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupGenerationShaper
{
  /// <summary>
  /// The class ShaperConfig is the aggreagate-root of the configuriation object tree.
  /// It is also the XML root element of my config elements. And represents a desierialised
  /// tree of the XML-Hierarchy.
  /// I implemented the code as found in 
  /// http://www.codeproject.com/Articles/16466/Unraveling-the-Mysteries-of-NET-Configuration
  /// Thanx to the author of this code....
  /// </summary>
  public class ShaperConfig : ConfigurationSection
  {
    static ShaperConfig()
    {
      // Predefine properties here
      s_ShaperDebugOptions = new ConfigurationProperty("DebugOptions", typeof(ShaperDebugOptions), new ShaperDebugOptions(), ConfigurationPropertyOptions.IsRequired);
      s_ShaperLogOptions = new ConfigurationProperty("LogOptions", typeof(ShaperLogOptions), new ShaperLogOptions(), ConfigurationPropertyOptions.IsRequired);
      s_ShaperDirectoryList = new ConfigurationProperty("DirectoryList", typeof(ShaperDirectoryCollection), new ShaperDirectoryCollection(), ConfigurationPropertyOptions.IsRequired);

      s_Properties = new ConfigurationPropertyCollection();
      s_Properties.Add(s_ShaperDebugOptions);
      s_Properties.Add(s_ShaperLogOptions);
      s_Properties.Add(s_ShaperDirectoryList);
    } //end     static ShaperConfig()

    #region Static Fields
    private static ConfigurationProperty s_ShaperDebugOptions;
    private static ConfigurationProperty s_ShaperLogOptions;
    private static ConfigurationProperty s_ShaperDirectoryList;
    private static ConfigurationPropertyCollection s_Properties;
    #endregion


    #region Properties

    [ConfigurationProperty("DebugOptions", IsRequired = true)]
    public ShaperDebugOptions DebugOptions => (ShaperDebugOptions)base[s_ShaperDebugOptions];

    [ConfigurationProperty("LogOptions", IsRequired = true)]
    public ShaperLogOptions LogOptions => (ShaperLogOptions)base[s_ShaperLogOptions];

    [ConfigurationProperty("DirectoryList", IsRequired = true)]
    public ShaperDirectoryCollection DirectoryList => (ShaperDirectoryCollection)base[s_ShaperDirectoryList];



    protected override ConfigurationPropertyCollection Properties => s_Properties;

    #endregion

  } //end public class ShaperConfig : ConfigurationSection


  /// <summary>
  /// Die ShaperDebugOptions sind ein Custom-Element der Shaper
  /// Config und sind deshalb so angelegt, damit das XML
  /// File eleganter aussieht. In der Klassenhierarchie
  /// gibt es dann im ShaperConfig eine Referenz auf die 
  /// ShaperDebugOptions
  /// </summary>
  public class ShaperDebugOptions : ConfigurationElement
  {
    /// <summary>
    /// Der statische ctor initialisiert auch hier das ganze Ding
    /// </summary>
    static ShaperDebugOptions()
    {
      s_IsSimulateOnly = new ConfigurationProperty("IsSimulateOnly", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);
      s_IsInteractive = new ConfigurationProperty("IsInteractive", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);
      s_OnExceptionSendMail = new ConfigurationProperty("OnExceptionSendMail", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);
      s_MailTo = new ConfigurationProperty("MailTo", typeof(string), "w.muellauer@mdc.at", ConfigurationPropertyOptions.None);
      s_MailFrom = new ConfigurationProperty("MailFrom", typeof(string), "spike.mdc@mdc.at", ConfigurationPropertyOptions.None);
      s_MailSMTP = new ConfigurationProperty("MailSMTP", typeof(string), "mail.mdc.at", ConfigurationPropertyOptions.None);

      s_Properties = new ConfigurationPropertyCollection();
      s_Properties.Add(s_IsSimulateOnly);
      s_Properties.Add(s_IsInteractive);
      s_Properties.Add(s_OnExceptionSendMail);
      s_Properties.Add(s_MailTo);
      s_Properties.Add(s_MailFrom);
      s_Properties.Add(s_MailSMTP);
    }

    #region Static Fields
    private static ConfigurationProperty s_IsSimulateOnly;
    private static ConfigurationProperty s_IsInteractive;
    private static ConfigurationProperty s_OnExceptionSendMail;
    private static ConfigurationProperty s_MailTo;
    private static ConfigurationProperty s_MailFrom;
    private static ConfigurationProperty s_MailSMTP;
    private static ConfigurationPropertyCollection s_Properties;
    #endregion


    #region Properties
    [ConfigurationProperty("IsSimulateOnly", IsRequired = true)]
    public bool IsSimulateOnly => (bool)base[s_IsSimulateOnly];

    [ConfigurationProperty("IsInteractive", IsRequired = true)]
    public bool IsInteractive => (bool)base[s_IsInteractive];

    [ConfigurationProperty("OnExceptionSendMail", IsRequired = true)]
    public bool OnExceptionSendMail => (bool)base[s_OnExceptionSendMail];

    [ConfigurationProperty("MailTo", IsRequired = false)]
    public string MailTo => (string)base[s_MailTo];

    [ConfigurationProperty("MailFrom", IsRequired = false)]
    public string MailFrom => (string)base[s_MailFrom];

    [ConfigurationProperty("MailSMTP", IsRequired = false)]
    public string MailSMTP => (string)base[s_MailSMTP];

    protected override ConfigurationPropertyCollection Properties => s_Properties;
    #endregion


  } //end public class ShaperDebugOptions : ConfigurationElement



  /// <summary>
  /// Die ShaperLogOptions definieren alle Aspekte des Logs:
  /// Ob geloggt weden soll und wo die Logfiles dann stehen sollen
  /// Die LogOptions sind dann auch wieder ein eigenes Element, 
  /// damit die Optionen besser organisiert sind.
  /// LogLevel is a string and might be 
  /// "Off", "Fatal", "Error", "Warn",  "Info", "Debug", "Trace"
  /// </summary>
  public class ShaperLogOptions : ConfigurationElement
  {
    /// <summary>
    /// Der statisch ctor initialisiert das ganze Ding wie üblich
    /// </summary>
    static ShaperLogOptions()
    {
      s_LogToScreen = new ConfigurationProperty("LogToScreen", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);
      s_LogToFile = new ConfigurationProperty("LogToFile", typeof(bool), false, ConfigurationPropertyOptions.IsRequired);
      s_LogDirectory = new ConfigurationProperty("LogDirectory", typeof(string), String.Empty, ConfigurationPropertyOptions.None);

      s_Properties = new ConfigurationPropertyCollection();
      s_Properties.Add(s_LogToScreen);
      s_Properties.Add(s_LogToFile);
      s_Properties.Add(s_LogDirectory);
    }


    #region Static Fields
    private static ConfigurationProperty s_LogToScreen;
    private static ConfigurationProperty s_LogToFile;
    private static ConfigurationProperty s_LogDirectory;
    private static ConfigurationPropertyCollection s_Properties;
    #endregion


    #region Properties
    [ConfigurationProperty("LogToScreen", IsRequired = true)]
    public bool LogToScreen => (bool)base[s_LogToScreen];

    [ConfigurationProperty("LogToFile", IsRequired = true)]
    public bool LogToFile => (bool)base[s_LogToFile];

    [ConfigurationProperty("LogDirectory", IsRequired = false)]
    public string LogDirectory => (string)base[s_LogDirectory];

    protected override ConfigurationPropertyCollection Properties => s_Properties;
    #endregion
  } //end public class ShaperLogOptions : ConfigurationElement



  /// <summary>
  /// Die Klasse ShaperDirectoryEntry beschreibt einen Eintrag für 
  /// ein Directory. Hier werden der DirectoryName, das Format für
  /// für den Timestamp und der GenerationCounter für diesen Directory
  /// Entry gespeichert. 
  /// </summary>
  public class ShaperDirectoryElement : ConfigurationElement
  {
    /// <summary>
    /// Der statisch ctor initialisiert das ganze Ding wie üblich
    /// </summary>
    static ShaperDirectoryElement()
    {
      s_DirectoryName = new ConfigurationProperty("DirectoryName", typeof(string), String.Empty, ConfigurationPropertyOptions.None);
      s_TimestampFormat = new ConfigurationProperty("TsFormat", typeof(string), String.Empty, ConfigurationPropertyOptions.None);
      s_GenerationCount = new ConfigurationProperty("GenerationCount", typeof(int), 0, ConfigurationPropertyOptions.None);

      s_Properties = new ConfigurationPropertyCollection();
      s_Properties.Add(s_DirectoryName);
      s_Properties.Add(s_TimestampFormat);
      s_Properties.Add(s_GenerationCount);
    }


    #region Static Fields
    private static ConfigurationProperty s_DirectoryName;
    private static ConfigurationProperty s_TimestampFormat;
    private static ConfigurationProperty s_GenerationCount;
    private static ConfigurationPropertyCollection s_Properties;
    #endregion



    #region Properties
    [ConfigurationProperty("DirectoryName", IsRequired = true)]
    public string DirectoryName => (string)base[s_DirectoryName];

    [ConfigurationProperty("TsFormat", IsRequired = true)]
    public string TimestampFormat => (string)base[s_TimestampFormat];

    [ConfigurationProperty("GenerationCount", IsRequired = true)]
    public int GenerationsCount => (int)base[s_GenerationCount];


    protected override ConfigurationPropertyCollection Properties => s_Properties;
    #endregion
  } //end   public class ShaperDirectoryElement : ConfigurationElement



  /// <summary>
  /// Die Klasse ShaperDirectoryCollection kapselt die Liste mit den
  /// Directories, ihren Timestamp-Formaten und den Countern in der Config.
  /// Damit entält das .config File eine Liste von Objekten.
  /// </summary>
  [ConfigurationCollection(typeof(ShaperDirectoryElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
  public class ShaperDirectoryCollection : ConfigurationElementCollection
  {
    /// <summary>
    /// Der statische ctor erzeugt wieder die Property Collection
    /// </summary>
#region Constructors
    static ShaperDirectoryCollection()
    {
      s_properties = new ConfigurationPropertyCollection();
    }

    /// <summary>
    /// normaler ctor macht nichts, muss es aber geben
    /// </summary>
    public ShaperDirectoryCollection()
    {
    }
    #endregion

    #region Fields
    private static ConfigurationPropertyCollection s_properties;
    #endregion


    #region Properties
    protected override ConfigurationPropertyCollection Properties => s_properties;
    public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMap;
    #endregion

    #region Indexers
    public ShaperDirectoryElement this[int index]
    {
      get { return (ShaperDirectoryElement)base.BaseGet(index); }
      set
      {
        if (base.BaseGet(index) != null) {
          base.BaseRemoveAt(index);
        }
        base.BaseAdd(index, value);
      }
    }

    public ShaperDirectoryElement this[string name] => (ShaperDirectoryElement)base.BaseGet(name);

    #endregion

    #region Overrides
    protected override ConfigurationElement CreateNewElement()
    {
      return new ShaperDirectoryElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return (element as ShaperDirectoryElement).DirectoryName;
    }
    #endregion



  } //end public class ShaperDirectoryCollection : ConfigurationElementCollection


  /// <summary>
  /// Diese Klasse entählt einige Tool-Methoden, die das Handling der Config-
  /// options erleichtern sollen.
  /// </summary>
  public class ShaperConfigTools
  {
    public static IEnumerable<string> EnumerateShaperConfig(ShaperConfig sc)
    {
      yield return "begin shaper config dump";

      yield return " debug options:";
      yield return $"  [IsSimulateOnly]:{sc.DebugOptions.IsSimulateOnly}";
      yield return $"  [IsInteractive]:{sc.DebugOptions.IsInteractive}";
      yield return $"  [OnExceptionSendMail]:{sc.DebugOptions.OnExceptionSendMail}";
      if (sc.DebugOptions.OnExceptionSendMail == true) {
        yield return $"  [MailTo]:{sc.DebugOptions.MailTo}";
        yield return $"  [MailFrom]:{sc.DebugOptions.MailFrom}";
        yield return $"  [MailSTMP]:{sc.DebugOptions.MailSMTP}";
      } else {
        yield return "  [MailTo]:IGNORED";
        yield return "  [MailFrom]:IGNORED";
        yield return "  [MailSTMP]:IGNORED";
      }
      yield return " log options:";
      yield return $"  [LogToScreen]:{sc.LogOptions.LogToScreen}";
      yield return $"  [LogToFile]:{sc.LogOptions.LogToFile}";
      if (sc.LogOptions.LogToFile == true) {
        yield return $"  [LogDirectory]:{sc.LogOptions.LogDirectory}";
      } else {
        yield return "  [LogDirectory]:IGNORED";
      }
      yield return " directory list:";
      foreach (ShaperDirectoryElement sde in sc.DirectoryList) {
        yield return $"  [Directory]:{sde.DirectoryName} [TimestampFormat]:{sde.TimestampFormat} [GenerationCount]:{sde.GenerationsCount}";
      }
      yield return "end shaper config dump";
      yield break;
    }


  } //end public class ShaperConfigTools


} //end namespace BackupGenerationShaper


/*

















*/
