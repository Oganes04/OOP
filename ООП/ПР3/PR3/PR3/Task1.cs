﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace PR3
{
    public enum CoolantSystemStatus { OK, Check, Fail }
    public enum SuccessFailureResult { Success, Fail }
    public class Switch
    {
        /// <summary>
        /// Utilty object for simulation
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// Disconnect from the external power generation systems
        /// </summary>
        /// <returns>Success or Failure</returns>
        /// <exception cref="PowerGeneratorCommsException">Thrown when the physical switch cannot establish a connection to the power generation system</exception>
        public SuccessFailureResult DisconnectPowerGenerator()
        {
            SuccessFailureResult r = SuccessFailureResult.Fail;
            if (rand.Next(1, 10) > 2) r = SuccessFailureResult.Success;
            if (rand.Next(1, 20) > 18) throw new PowerGeneratorCommsException("Network failure accessing Power Generator monitoring system");
            return r;
        }
        /// <summary>
        /// Runs a diagnostic check against the primary coolant system
        /// </summary>
        /// <returns>Coolant System Status (OK, Fail, Check)</returns>
        /// <exception cref="CoolantTemperatureReadException">Thrown when the switch cannot read the temperature from the coolant system</exception>
        /// <exception cref="CoolantPressureReadException">Thrown when the switch cannot read the pressure from the coolant system</exception>
        public CoolantSystemStatus VerifyPrimaryCoolantSystem()
        {
            CoolantSystemStatus c = CoolantSystemStatus.Fail;
            int r = rand.Next(1, 10);
            if (r > 5)
            {
                c = CoolantSystemStatus.OK;
            }
            else if (r > 2)
            {
                c = CoolantSystemStatus.Check;
            }
            if (rand.Next(1, 20) > 18) throw new CoolantTemperatureReadException("Failed to read primary coolant system temperature");
            if (rand.Next(1, 20) > 18) throw new CoolantPressureReadException("Failed to read primary coolant system pressure");
            return c;
        }
        /// <summary>
        /// Runs a diagnostic check against the backup coolant system
        /// </summary>
        /// <returns>Coolant System Status (OK, Fail, Check)</returns>
        /// <exception cref="CoolantTemperatureReadException">Thrown when the switch cannot read the temperature from the coolant system</exception>
        /// <exception cref="CoolantPressureReadException">Thrown when the switch cannot read the pressure from the coolant system</exception>
        public CoolantSystemStatus VerifyBackupCoolantSystem()
        {
            CoolantSystemStatus c = CoolantSystemStatus.Fail;
            int r = rand.Next(1, 10);
            if (r > 5)
            {
                c = CoolantSystemStatus.OK;
            }
            else if (r > 2)
            {
                c = CoolantSystemStatus.Check;
            }
            if (rand.Next(1, 20) > 19) throw new CoolantTemperatureReadException("Failed to read backup coolant system temperature");
            if (rand.Next(1, 20) > 19) throw new CoolantPressureReadException("Failed to read backup coolant system pressure");
            return c;
        }

        /// <summary>
        /// Reads the temperature from the reactor core
        /// </summary>
        /// <returns>Temperature</returns>
        /// <exception cref="CoreTemperatureReadException">Thrown when the switch cannot access the temperature data</exception>
        public double GetCoreTemperature()
        {
            if (rand.Next(1, 20) > 18) throw new CoreTemperatureReadException("Failed to read core reactor system temperature");
            return rand.NextDouble() * 1000;
        }

        /// <summary>
        /// Instructs the reactor to insert the control rods to shut the reactor down
        /// </summary>
        /// <returns>Success or failure</returns>
        /// <exception cref="RodClusterReleaseException">Thrown if the switch device cannot read the rod position</exception>
        public SuccessFailureResult InsertRodCluster()
        {
            SuccessFailureResult r = SuccessFailureResult.Fail;
            if (rand.Next(1, 100) > 5) r = SuccessFailureResult.Success;
            if (rand.Next(1, 10) > 8) throw new RodClusterReleaseException("Sensor failure, cannot verify rod release");
            return r;
        }

        /// <summary>
        /// Reads the radiation level from the reactor core
        /// </summary>
        /// <returns>Temperature</returns>
        /// <exception cref="CoreRadiationLevelReadException">Thrown when the switch cannot access the radiation level data</exception>
        public double GetRadiationLevel()
        {
            if (rand.Next(1, 20) > 18) throw new CoreRadiationLevelReadException("Failed to read core reactor system radiation levels");
            return rand.NextDouble() * 500;
        }

        /// <summary>
        /// Sends a broadcast message to PA system notofying shutdown complete
        /// </summary>
        /// <exception cref="SignallingException">Thrown if the switch cannot connect to the PA system over the network</exception>
        public void SignalShutdownComplete()
        {
            if (rand.Next(1, 20) > 18) throw new SignallingException("Network failure connecting to broadcast systems");
        }
    }
    public class PowerGeneratorCommsException : Exception
    {
        public PowerGeneratorCommsException(string message) : base(message) { }
    }
    public class CoolantSystemException : Exception
    {
        public CoolantSystemException(string message) : base(message) { }
    }
    public class CoolantTemperatureReadException : CoolantSystemException
    {
        public CoolantTemperatureReadException(string message) : base(message) { }
    }
    public class CoolantPressureReadException : CoolantSystemException
    {
        public CoolantPressureReadException(string message) : base(message) { }
    }
    public class CoreTemperatureReadException : Exception
    {
        public CoreTemperatureReadException(string message) : base(message) { }
    }
    public class CoreRadiationLevelReadException : Exception
    {
        public CoreRadiationLevelReadException(string message) : base(message) { }
    }
    public class RodClusterReleaseException : Exception
    {
        public RodClusterReleaseException(string message) : base(message) { }
    }
    public class SignallingException : Exception
    {
        public SignallingException(string message) : base(message) { }
    }

    internal class Task1
    {
        /// <summary>
        /// Пытается отключить воображаемый генератор питания.
        /// </summary>
        /// <param name="reactor_switch">Объект типа Switch, представляющий переключатель реактора.</param>
        /// <param name="error">Сообщение об ошибке, если отключение не удалось.</param>
        /// <returns>True, если отключение прошло успешно, false в противном случае.</returns>
        /// <exception cref="PowerGeneratorCommsException">Выбрасывается, если возникает ошибка связи с генератором питания.</exception>
        public static bool TryDisconnectPowerGenerator(Switch reactor_switch, out string error)
        {
            error = "";
            try
            {
                return (reactor_switch.DisconnectPowerGenerator() == SuccessFailureResult.Success);
            }
            catch (PowerGeneratorCommsException e)
            {
                error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Пытается проверить воображаемую резервную систему охлаждения.
        /// </summary>
        /// <param name="reactor_switch">Объект типа Switch, представляющий переключатель реактора.</param>
        /// <param name="error">Сообщение об ошибке, если проверка не удалась.</param>
        /// <returns>True, если резервная система охлаждения работает исправно, false в противном случае.</returns>
        /// <exception cref="CoolantTemperatureReadException">Выбрасывается, если возникает ошибка чтения температуры охлаждающей жидкости.</exception>
        /// <exception cref="CoolantPressureReadException">Выбрасывается, если возникает ошибка чтения давления охлаждающей жидкости.</exception>
        public static bool TryVerifyBackupCoolantSystem(Switch reactor_switch, out string error)
        {
            error = "";
            try
            {
                CoolantSystemStatus status = reactor_switch.VerifyBackupCoolantSystem();
                switch (status)
                {
                    case CoolantSystemStatus.OK:
                        return true;
                    case CoolantSystemStatus.Check:
                        error = "Необходимо проверить охлаждающую жидкость";
                        return false;
                    case CoolantSystemStatus.Fail:
                        return false;
                }

            }
            catch (CoolantTemperatureReadException e)
            {
                error = e.Message;
                return false;
            }
            catch (CoolantPressureReadException e)
            {
                error = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Пытается получить текущую температуру воображаемого ядра реактора.
        /// </summary>
        /// <param name="reactor_switch">Объект типа Switch, представляющий переключатель реактора.</param>
        /// <param name="temparature">Текущая температура ядра реактора.</param>
        /// <param name="error">Сообщение об ошибке, если чтение температуры не удалось.</param>
        /// <returns>True, если чтение температуры прошло успешно, false в противном случае.</returns>
        /// <exception cref="CoreTemperatureReadException">Выбрасывается, если возникает ошибка чтения температуры ядра реактора.</exception>
        public static bool TryGetCoreTemperature(Switch reactor_switch, out double temparature, out string error)
        {
            temparature = 0;
            error = "";
            try
            {
                temparature = reactor_switch.GetCoreTemperature();
                return true;
            }
            catch (CoreTemperatureReadException e)
            {
                error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Пытается вставить воображаемый кластер стержней в реактор.
        /// </summary>
        /// <param name="reactor_switch">Объект типа Switch, представляющий переключатель реактора.</param>
        /// <param name="error">Сообщение об ошибке, если вставка не удалась.</param>
        /// <returns>True, если вставка прошла успешно, false в противном случае.
        /// Выбрасывается, если возникает ошибка при выпуске кластера стержней.
        public static bool TryInsertRodCluster(Switch reactor_switch, out string error)
        {
            error = "";
            try
            {
                return reactor_switch.InsertRodCluster() == SuccessFailureResult.Success;
            }
            catch (RodClusterReleaseException e)
            {
                error = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Пытается получить текущий уровень радиации в реакторе.
        /// </summary>
        /// <param name="reactor_switch">Объект типа Switch, представляющий переключатель реактора.</param>
        /// <param name="radiation">Текущий уровень радиации в реакторе.</param>
        /// <param name="error">Сообщение об ошибке, если чтение уровня радиации не удалось.</param>
        /// <returns>True, если чтение уровня радиации прошло успешно, false в противном случае.</returns>
        /// <exception cref="CoreRadiationLevelReadException">Выбрасывается, если возникает ошибка чтения уровня радиации в реакторе.</exception>
        public static bool TryGetRadiationLevel(Switch reactor_switch, out double radiation, out string error)
        {
            error = "";
            radiation = 0;
            try
            {
                radiation = reactor_switch.GetRadiationLevel();
                return true;
            }
            catch (CoreRadiationLevelReadException e)
            {
                error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Отправляет сигнал о завершении работы реактора.
        /// </summary>
        /// <param name="reactor_switch">Объект типа Switch, представляющий переключатель реактора.</param>
        /// <param name="error">Сообщение об ошибке, если отправка сигнала не удалась.</param>
        /// <returns>True, если сигнал был успешно отправлен, false в противном случае.</returns>
        public static bool TrySignalShutdownComplete(Switch reactor_switch, out string error)
        {
            error = "";
            try
            {
                reactor_switch.SignalShutdownComplete();
                return true;
            }
            catch (SignallingException e)
            {
                error = e.Message;
                return false;
            }
        }
    }
}
