using Data;
using System.Diagnostics;
using System.Numerics;


namespace Logic
{
	internal class Ball : IBall
	{
		private List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();
		private Data.IBall ballData;
		private DataAPI _data = DataAPI.createInstance();
		private List<IBall> otherBalls;

		public Ball(Data.IBall ballData, List<IBall> otherBalls)
		{
			this.ballData = ballData;
			this.otherBalls = otherBalls;
			ballData.Subscribe(this);
		}

		public Vector2 getPosition()
		{
			return ballData.getPosition();
		}

		public void setVelocity(Vector2 newVelocity)
		{
			ballData.setVelocity(newVelocity);
		}

		public Vector2 getVelocity()
		{
			return ballData.getVelocity();
		}

		public IDisposable Subscribe(IObserver<Vector2> observer)
		{
			if (!observers.Contains(observer))
				observers.Add(observer);
			return new Unsubscriber(observers, observer);
		}

		private class Unsubscriber : IDisposable
		{
			private List<IObserver<Vector2>> _observers;
			private IObserver<Vector2> _observer;

			public Unsubscriber(List<IObserver<Vector2>> observers, IObserver<Vector2> observer)
			{
				_observers = observers;
				_observer = observer;
			}

			public void Dispose()
			{
				if (!(_observer == null)) _observers.Remove(_observer);
			}
		}

		public void OnCompleted()
		{

		}

		public void OnError(Exception error)
		{

		}

		private void handleCollisionsWithWalls()
		{
			if (getPosition().X + getVelocity().X * ballData.getElapsedTimeInSeconds() > _data.width - _data.radius - 20)
			{
				setVelocity(new Vector2(getVelocity().X * (-1), getVelocity().Y));
			}

			if (getPosition().X + getVelocity().X * ballData.getElapsedTimeInSeconds() < 0 + _data.radius)
			{
				setVelocity(new Vector2(getVelocity().X * (-1), getVelocity().Y));
			}

			if (getPosition().Y + getVelocity().Y * ballData.getElapsedTimeInSeconds() > _data.height - _data.radius - 20)
			{
				setVelocity(new Vector2(getVelocity().X, getVelocity().Y * (-1)));
			}

			if (getPosition().Y + getVelocity().Y * ballData.getElapsedTimeInSeconds() < 0 + _data.radius)
			{
				setVelocity(new Vector2(getVelocity().X, getVelocity().Y * (-1)));
			}
		}

		public float getElapsedTimeInSeconds()
		{
			return ballData.getElapsedTimeInSeconds();
		}


        private void handleCollisionsWithBalls()
		{
			lock(otherBalls)
			{
				for (int i = 0; i < otherBalls.Count; i++)
				{
					if (otherBalls[i] == this) { continue; }
					float distance = Vector2.Distance(getPosition() + getVelocity() * ballData.getElapsedTimeInSeconds(), otherBalls[i].getPosition() + otherBalls[i].getVelocity() * otherBalls[i].getElapsedTimeInSeconds());
					if (distance <= _data.radius * 2)
					{ 
						Vector2 relativeVelocity = getVelocity() - otherBalls[i].getVelocity();
						Vector2 unitNormal = Vector2.Normalize(getPosition() - otherBalls[i].getPosition());
						float dotProduct = Vector2.Dot(relativeVelocity, unitNormal);
						Vector2 impulse = dotProduct * unitNormal * (2 * _data.weight * _data.weight / (_data.weight + _data.weight));

						setVelocity(getVelocity() - impulse / _data.weight);
                        otherBalls[i].setVelocity(otherBalls[i].getVelocity() + impulse / _data.weight);
					}
				}
			}
		}

		public void OnNext(Vector2 value)
		{
			handleCollisionsWithWalls();
			handleCollisionsWithBalls();
			foreach (IObserver<Vector2> observer in observers)
			{
				observer.OnNext(value);
			}
		}

		public int getRadius()
		{
			return _data.radius;
		}

	}
}
