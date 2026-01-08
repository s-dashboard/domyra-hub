
interface HourlyForecast {
  time: string;
  temperature: number;
  weatherCode: number;
}

interface DailyForecast {
  date: string;
  min: number;
  max: number;
  weatherCode: number;
}

interface OpenMeteoResponse {
  hourly: {
    time: string[];
    temperature_2m: number[];
    weathercode: number[];
  };
  daily: {
    time: string[];
    temperature_2m_min: number[];
    temperature_2m_max: number[];
    weathercode: number[];
  };
}

interface LocalTemperature {
    current: number, 
    max: number,
    min: number,
}