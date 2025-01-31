﻿using Colossal.UI.Binding;
using ExtendedTooltip.Settings;
using ExtendedTooltip.src.Settings;
using Game.UI;
using System;
using System.Collections.Generic;

namespace ExtendedTooltip.Systems
{
    class ExtendedTooltipUISystem : UISystemBase
    {
        private readonly string kGroup = "extendedTooltip";
        private ExtendedTooltipSystem m_ExtendedTooltipSystem;
        private CustomTranslationSystem m_CustomTranslationSystem;
        private LocalSettingsItem m_Settings;
        private Dictionary<string, string> m_SettingLocalization;

        private Dictionary<SettingKey, Action> toggleActions;
        private Dictionary<SettingKey, Action> expandActions;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_ExtendedTooltipSystem = World.GetOrCreateSystemManaged<ExtendedTooltipSystem>();
            m_CustomTranslationSystem = World.GetOrCreateSystemManaged<CustomTranslationSystem>();
            m_Settings = m_ExtendedTooltipSystem.m_LocalSettings.Settings;
            
            CreateActions();
            CreateLanguages();
            CreateBindings();

            UnityEngine.Debug.Log("ExtendedTooltipUISystem created.");
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnityEngine.Debug.Log("ExtendedTooltipUISystem destroyed.");
        }

        private void CreateActions()
        {
            toggleActions = new()
            {
                { SettingKey.Citizen, () => m_Settings.Citizen = !m_Settings.Citizen },
                { SettingKey.CitizenState, () => m_Settings.CitizenState = !m_Settings.CitizenState },
                { SettingKey.CitizenHappiness, () => m_Settings.CitizenHappiness = !m_Settings.CitizenHappiness },
                { SettingKey.CitizenEducation, () => m_Settings.CitizenEducation = !m_Settings.CitizenEducation },
                { SettingKey.Company, () => m_Settings.Company = !m_Settings.Company },
                { SettingKey.CompanyOutput, () => m_Settings.CompanyOutput = !m_Settings.CompanyOutput },
                { SettingKey.Efficiency, () => m_Settings.Efficiency = !m_Settings.Efficiency },
                { SettingKey.Employee, () => m_Settings.Employee = !m_Settings.Employee },
                { SettingKey.ParkingFacility, () => m_Settings.ParkingFacility = !m_Settings.ParkingFacility },
                { SettingKey.ParkingFees, () => m_Settings.ParkingFees = !m_Settings.ParkingFees },
                { SettingKey.ParkingCapacity, () => m_Settings.ParkingCapacity = !m_Settings.ParkingCapacity },
                { SettingKey.Park, () => m_Settings.Park = !m_Settings.Park },
                { SettingKey.ParkMaintenance, () => m_Settings.ParkMaintenance = !m_Settings.ParkMaintenance },
                { SettingKey.PublicTransport, () => m_Settings.PublicTransport = !m_Settings.PublicTransport },
                { SettingKey.PublicTransportWaitingPassengers, () => m_Settings.PublicTransportWaitingPassengers = !m_Settings.PublicTransportWaitingPassengers },
                { SettingKey.PublicTransportWaitingTime, () => m_Settings.PublicTransportWaitingTime = !m_Settings.PublicTransportWaitingTime },
                { SettingKey.Road, () => m_Settings.Road = !m_Settings.Road },
                { SettingKey.RoadLength, () => m_Settings.RoadLength = !m_Settings.RoadLength },
                { SettingKey.RoadUpkeep, () => m_Settings.RoadUpkeep = !m_Settings.RoadUpkeep },
                { SettingKey.RoadCondition, () => m_Settings.RoadCondition = !m_Settings.RoadCondition },
                { SettingKey.School, () => m_Settings.School = !m_Settings.School },
                { SettingKey.SchoolStudentCapacity, () => m_Settings.SchoolStudentCapacity = !m_Settings.SchoolStudentCapacity },
                { SettingKey.Spawnable, () => m_Settings.Spawnable = !m_Settings.Spawnable },
                { SettingKey.SpawnableLevel, () => m_Settings.SpawnableLevel = !m_Settings.SpawnableLevel },
                { SettingKey.SpawnableLevelDetails, () => m_Settings.SpawnableLevelDetails = !m_Settings.SpawnableLevelDetails },
                { SettingKey.SpawnableHousehold, () => m_Settings.SpawnableHousehold = !m_Settings.SpawnableHousehold },
                { SettingKey.SpawnableHouseholdDetails, () => m_Settings.SpawnableHouseholdDetails = !m_Settings.SpawnableHouseholdDetails },
                { SettingKey.SpawnableRent, () => m_Settings.SpawnableRent = !m_Settings.SpawnableRent },
                { SettingKey.SpawnableBalance, () => m_Settings.SpawnableBalance = !m_Settings.SpawnableBalance },
                { SettingKey.SpawnableZoneInfo, () => m_Settings.SpawnableZoneInfo = !m_Settings.SpawnableZoneInfo },
                { SettingKey.Vehicle, () => m_Settings.Vehicle = !m_Settings.Vehicle },
                { SettingKey.VehiclePassengerDetails, () => m_Settings.VehiclePassengerDetails = !m_Settings.VehiclePassengerDetails },
                { SettingKey.VehicleDriver, () => m_Settings.VehicleDriver = !m_Settings.VehicleDriver },
                { SettingKey.VehicleState, () => m_Settings.VehicleState = !m_Settings.VehicleState },
                { SettingKey.VehiclePostvan, () => m_Settings.VehiclePostvan = !m_Settings.VehiclePostvan},
                { SettingKey.VehicleGarbageTruck, () => m_Settings.VehicleGarbageTruck = !m_Settings.VehicleGarbageTruck },
                { SettingKey.DisableMod, () => m_Settings.DisableMod = !m_Settings.DisableMod },
                { SettingKey.AnarchyMode, () => m_Settings.AnarchyMode = !m_Settings.AnarchyMode },
                { SettingKey.NetToolSystem, () => m_Settings.NetToolSystem = !m_Settings.NetToolSystem },
                { SettingKey.NetToolMode, () => m_Settings.NetToolMode = !m_Settings.NetToolMode },
                { SettingKey.NetToolElevation, () => m_Settings.NetToolElevation = !m_Settings.NetToolElevation },
                { SettingKey.ExtendedLayout, () => m_Settings.ExtendedLayout = !m_Settings.ExtendedLayout },
            };

            expandActions = new()
            {
                { SettingKey.NetToolSystem, () => m_Settings.NetToolSystemExpanded = !m_Settings.NetToolSystemExpanded },
                { SettingKey.Citizen, () => m_Settings.CitizenExpanded = !m_Settings.CitizenExpanded },
                { SettingKey.Company, () => m_Settings.CompanyExpanded = !m_Settings.CompanyExpanded },
                { SettingKey.ParkingFacility, () => m_Settings.ParkingExpanded = !m_Settings.ParkingExpanded },
                { SettingKey.Park, () => m_Settings.ParkExpanded = !m_Settings.ParkExpanded },
                { SettingKey.PublicTransport, () => m_Settings.PublicTransportExpanded = !m_Settings.PublicTransportExpanded },
                { SettingKey.Road, () => m_Settings.RoadExpanded = !m_Settings.RoadExpanded },
                { SettingKey.School, () => m_Settings.SchoolExpanded = !m_Settings.SchoolExpanded },
                { SettingKey.Spawnable, () => m_Settings.SpawnableExpanded = !m_Settings.SpawnableExpanded },
                { SettingKey.Vehicle, () => m_Settings.VehicleExpanded = !m_Settings.VehicleExpanded }
            };
        }

        private void CreateBindings()
        {
            AddUpdateBinding(new GetterValueBinding<Dictionary<string, string>>(kGroup, "translations", () => m_SettingLocalization, new DictionaryWriter<string, string>(null, null).Nullable(), null));
            AddUpdateBinding(new GetterValueBinding<string>(kGroup, "version", () => MyPluginInfo.PLUGIN_VERSION, null, null));

            /// GENERAL
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "disableMod", () => m_Settings.DisableMod, null, null));
            AddUpdateBinding(new GetterValueBinding<int>(kGroup, "displayMode", () => (int)m_Settings.DisplayMode, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "extendedLayout", () => m_Settings.ExtendedLayout, null, null));
            AddBinding(new TriggerBinding<int>(kGroup, "onDisplayModeSelect", OnDisplayModeSelect));

            /// TOOL SYSTEM
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "anarchyMode", () => m_Settings.AnarchyMode, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "netToolSystem", () => m_Settings.NetToolSystem, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandNetToolSystem", () => m_Settings.NetToolSystemExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "netToolMode", () => m_Settings.NetToolMode, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "netToolElevation", () => m_Settings.NetToolElevation, null, null));

            /// CITIZENS
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "citizen", () => m_Settings.Citizen, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandCitizen", () => m_Settings.CitizenExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "citizenState", () => m_Settings.CitizenState, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "citizenHappiness", () => m_Settings.CitizenHappiness, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "citizenEducation", () => m_Settings.CitizenEducation, null, null));

            /// COMPANIES
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "company", () => m_Settings.Company, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandCompany", () => m_Settings.CompanyExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "companyOutput", () => m_Settings.CompanyOutput, null, null));

            /// EFFICIENCY
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "efficiency", () => m_Settings.Efficiency, null, null));

            /// EMPLOYEES
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "employee", () => m_Settings.Employee, null, null));

            /// PARKS
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "park", () => m_Settings.Park, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandPark", () => m_Settings.ParkExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "parkMaintenance", () => m_Settings.ParkMaintenance, null, null));

            /// PARKING
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "parkingFacility", () => m_Settings.ParkingFacility, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandParking", () => m_Settings.ParkingExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "parkingFees", () => m_Settings.ParkingFees, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "parkingCapacity", () => m_Settings.ParkingCapacity, null, null));

            /// PUBLIC TRANSPORT
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "publicTransport", () => m_Settings.PublicTransport, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandPublicTransport", () => m_Settings.PublicTransportExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "publicTransportWaitingPassengers", () => m_Settings.PublicTransportWaitingPassengers, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "publicTransportWaitingTime", () => m_Settings.PublicTransportWaitingTime, null, null));

            /// ROAD
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "road", () => m_Settings.Road, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandRoad", () => m_Settings.RoadExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "roadLength", () => m_Settings.RoadLength, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "roadUpkeep", () => m_Settings.RoadUpkeep, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "roadCondition", () => m_Settings.RoadCondition, null, null));

            /// SCHOOL
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "school", () => m_Settings.School, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandSchool", () => m_Settings.SchoolExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "schoolStudentCapacity", () => m_Settings.SchoolStudentCapacity, null, null));

            /// SPAWNABLE
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnable", () => m_Settings.Spawnable, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandSpawnable", () => m_Settings.SpawnableExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableZoneInfo", () => m_Settings.SpawnableZoneInfo, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableHousehold", () => m_Settings.SpawnableHousehold, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableHouseholdDetails", () => m_Settings.SpawnableHouseholdDetails, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableLevel", () => m_Settings.SpawnableLevel, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableLevelDetails", () => m_Settings.SpawnableLevelDetails, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableRent", () => m_Settings.SpawnableRent, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "spawnableBalance", () => m_Settings.SpawnableBalance, null, null));

            /// VEHICLE
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "vehicle", () => m_Settings.Vehicle, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "expandVehicle", () => m_Settings.VehicleExpanded, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "vehicleState", () => m_Settings.VehicleState, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "vehicleDriver", () => m_Settings.VehicleDriver, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "vehicleGarbageTruck", () => m_Settings.VehicleGarbageTruck, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "vehiclePostvan", () => m_Settings.VehiclePostvan, null, null));
            AddUpdateBinding(new GetterValueBinding<bool>(kGroup, "vehiclePassengerDetails", () => m_Settings.VehiclePassengerDetails, null, null));

            AddBinding(new TriggerBinding<int>(kGroup, "onToggle", OnToggle));
            AddBinding(new TriggerBinding<int>(kGroup, "onExpand", OnExpand));
        }

        private void CreateLanguages() {
            m_SettingLocalization = new()
            {
                // GENERAL
                { "disableMod", m_CustomTranslationSystem.GetTranslation("setting.disableMod", "Disable Mod") },
                { "disableMod.description", m_CustomTranslationSystem.GetTranslation("setting.disableMod.description", "Disable the mod globally.") },
                { "displayMode", m_CustomTranslationSystem.GetTranslation("setting.displayMode", "Display mode") },
                { "displayMode.description", m_CustomTranslationSystem.GetTranslation("setting.displayMode.description", "Decide between different display modes for the tooltip.") },
                { "displayMode.instant", m_CustomTranslationSystem.GetTranslation("setting.displayMode.instant", "Instant (default)") },
                { "displayMode.delayed", m_CustomTranslationSystem.GetTranslation("setting.displayMode.delayed", "Delayed") },
                { "displayMode.onKey", m_CustomTranslationSystem.GetTranslation("setting.displayMode.onKey", "Hold key (ALT)") },
                { "extendedLayout", m_CustomTranslationSystem.GetTranslation("setting.extendedLayout", "Extended Layout") },
                { "extendedLayout.description", m_CustomTranslationSystem.GetTranslation("setting.extendedLayout.description", "Use a second layout group to show tooltips.") },

                // TOOL SYSTEMS
                { "toolSystem", m_CustomTranslationSystem.GetTranslation("setting.toolSystem", "Tool Systems") },
                { "toolSystem.description", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.description", "Enable tooltips for different tool systems.") },

                // NET TOOL SYSTEM

                { "toolSystem.anarchyMode", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.anarchyMode", "Anarchy") },
                { "toolSystem.anarchyMode.description", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.anarchyMode.description", "Shows anarchy status.") },

                { "toolSystem.netTool", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.netTool", "Net Tool") },
                { "toolSystem.netTool.description", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.netTool.description", "Enable tooltips for the Net Tool.") },
                { "toolSystem.netTool.mode", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.netTool.mode", "Net Tool Mode") },
                { "toolSystem.netTool.mode.description", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.netTool.mode.description", "Shows which mode (straight, curve, ect.) is in use.") },
                { "toolSystem.netTool.elevation", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.netTool.elevation", "Elevation") },
                { "toolSystem.netTool.elevation.description", m_CustomTranslationSystem.GetTranslation("setting.toolSystem.netTool.elevation.description", "Adds elevation to net tool mode tooltip.") },

                // ENTITY TOOLTIPS
                { "entities", m_CustomTranslationSystem.GetTranslation("entities", "Entities") },
                { "entities.description", m_CustomTranslationSystem.GetTranslation("entities.description", "Enables entity tooltips. (Citizen, Vehicles, Buildings, Spawnings, ect.)") },

                // CITIZEN
                { "citizen", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.CITIZEN_TYPE[Citizen]", "Citizens") },
                { "citizenState", m_CustomTranslationSystem.GetTranslation("setting.citizen.state", "Citizen state") },
                { "citizenHappiness", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.CITIZEN_HAPPINESS", "Citizen happiness") },
                { "citizenEducation", m_CustomTranslationSystem.GetLocalGameTranslation("Infoviews.INFOVIEW[Education]", "Educational Facilities") },

                // COMPANY
                { "company", m_CustomTranslationSystem.GetTranslation("setting.company", "Company") },
                { "companyOutput", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.PRODUCTION", "Company Output") },

                // EFFICIENCY
                { "efficiency", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.EFFICIENCY", "Efficiency")},

                // EMPLOYEE
                { "employee", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.EMPLOYEES", "Employee")},

                // PARK
                { "park", m_CustomTranslationSystem.GetLocalGameTranslation("Services.NAME[Parks & Recreation]", "Parks")},
                { "parkMaintenance", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.PARK_MAINTENANCE", "Maintenance")},
                
                // PARKING
                { "parkingFacility", m_CustomTranslationSystem.GetLocalGameTranslation("SubServices.NAME[RoadsParking]", "Parking Facilities")},
                { "parkingFees", m_CustomTranslationSystem.GetTranslation("setting.parking.fees", "Fees")},
                { "parkingCapacity", m_CustomTranslationSystem.GetTranslation("setting.parking.capacity", "Capacity")},
                
                // PUBLIC TRANSPORT
                { "publicTransport", m_CustomTranslationSystem.GetLocalGameTranslation("TransportInfoPanel.PUBLIC_TRANSPORT_TITLE", "Public Transport")},
                { "publicTransportWaitingPassengers", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.WAITING_PASSENGERS", "Waiting Passengers")},
                { "publicTransportWaitingTime", m_CustomTranslationSystem.GetTranslation("setting.public_transportation.waiting_time", "Waiting Time")},

                // ROAD
                { "road", m_CustomTranslationSystem.GetLocalGameTranslation("Services.NAME[Roads]", "Roads")},
                { "roadLength", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.ROAD_LENGTH", "Length")},
                { "roadUpkeep", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.ROAD_UPKEEP", "Upkeep")},
                { "roadCondition", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.ROAD_CONDITION", "Condition")},
                
                // SCHOOL
                { "school", m_CustomTranslationSystem.GetLocalGameTranslation("SubServices.NAME[Education]", "Educational Facilities")},
                { "schoolStudentCapacity", m_CustomTranslationSystem.GetTranslation("setting.school.student.capacity", "Students Capacity")},
                
                // SPAWNABLE
                { "spawnable", m_CustomTranslationSystem.GetLocalGameTranslation("Services.NAME[Zones]", "Spawnable")},
                { "spawnableZoneInfo", m_CustomTranslationSystem.GetTranslation("setting.spawnable.zone_info", "Zone Info")},
                { "spawnableLevel", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.LEVEL", "Level")},
                { "spawnableLevelDetails", m_CustomTranslationSystem.GetTranslation("setting.spawnable.level_details", "Level Detail")},
                { "spawnableHousehold", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.HOUSEHOLDS", "Households")},
                { "spawnableHouseholdDetails", m_CustomTranslationSystem.GetTranslation("setting.spawnable.household_details", "Household Details")},
                { "spawnableRent", m_CustomTranslationSystem.GetTranslation("rent", "Rent")},
                { "spawnableBalance", m_CustomTranslationSystem.GetTranslation("balance", "Balance")},

                // VEHICLE
                { "vehicle", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.VEHICLES[HouseholdVehicle]", "Vehicles")},
                { "vehicleState", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.VEHICLE_STATE", "Status")},
                { "vehicleDriver", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.DRIVER", "Driver")},
                { "vehicleGarbageTruck", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.GARBAGE_VEHICLE_TITLE[GarbageTruck]", "Garbage Truck")},
                { "vehiclePostvan", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.POST_VEHICLE_TITLE", "Postvan")},
                { "vehiclePassengerDetail", m_CustomTranslationSystem.GetLocalGameTranslation("SelectedInfoPanel.PASSENGERS_TITLE", "Passengers")},

            };
        }
        private void OnDisplayModeSelect(int displayMode)
        {
            UnityEngine.Debug.Log($"DisplayMode changed to {displayMode}");
            m_Settings.DisplayMode = (DisplayMode) displayMode;
        }

        private async void OnToggle(int settingId)
        {
            if (toggleActions.TryGetValue((SettingKey)Enum.ToObject(typeof(SettingKey), settingId), out Action toggleAction))
            {
                toggleAction.Invoke();
                await m_ExtendedTooltipSystem.m_LocalSettings.Save();
            }
            else
            {
                UnityEngine.Debug.Log($"Toggle Setting with Id {settingId} not found.");
            }
        }

        private async void OnExpand(int settingId)
        {
            if (expandActions.TryGetValue((SettingKey) Enum.ToObject(typeof(SettingKey), settingId), out Action expandAction))
            {
                expandAction.Invoke();
                await m_ExtendedTooltipSystem.m_LocalSettings.Save();
            }
            else
            {
                UnityEngine.Debug.Log($"Expand setting with Id {settingId} not found.");
            }
        }
    }
}
