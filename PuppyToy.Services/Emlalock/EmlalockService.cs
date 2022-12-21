using PuppyToy.Models.Enum;
using PuppyToy.Models.Storable;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PuppyToy.Services.Emlalock
{
    public class EmlalockService
    {
        public ConcurrentDictionary<string, EmlalockFeedItem> FeedItems
        {
            get;
            set;
        } = new ConcurrentDictionary<string, EmlalockFeedItem>();

        private Task? _backgroundTask = null;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private EmlalockFeedReader _emlalockFeedReader;

        public event EventHandler<EmlalockEventArgs> OnNewFeedItem;

        public event EventHandler<EmlalockEventArgs> OnNewVote;

        public event EventHandler<EmlalockEventArgs> OnNewAddVote;

        public event EventHandler<EmlalockEventArgs> OnNewRemoveVote;

        public event EventHandler<EmlalockEventArgs> OnPilloryEntered;

        public event EventHandler<EmlalockEventArgs> OnPilloryVoted;

        public event EventHandler<EmlalockEventArgs> OnSelfAdded;

        public event EventHandler<EmlalockEventArgs> OnApiAdded;

        public event EventHandler<EmlalockEventArgs> OnApiRemoved;

        public event EventHandler<EmlalockEventArgs> OnWheelOfFortune;

        public event EventHandler<EmlalockEventArgs> OnInsufficientGames;

        public event EventHandler<EmlalockEventArgs> OnRemainingDuration;

        public event EventHandler<EmlalockEventArgs> OnHolderChanged;

        public event EventHandler<EmlalockEventArgs> OnHolderAdded;

        public event EventHandler<EmlalockEventArgs> OnHolderRemoved;

        public EmlalockService(string? feedUrl)
        {
            _emlalockFeedReader = new EmlalockFeedReader(feedUrl);
            OnNewFeedItem += OnOnNewFeedItem;
        }

        public void StartTask()
        {
            if (_backgroundTask != null)
            {
                throw new ApplicationException("Task is already set");
            }

            _backgroundTask = Task.Run(Run, _cancellationTokenSource.Token);
        }

        public void StopTask()
        {
            if (_backgroundTask == null)
            {
                throw new ApplicationException("Task is not set");
            }

            _cancellationTokenSource.Cancel();
        }

        private void OnOnNewFeedItem(object? sender, EmlalockEventArgs e)
        {
            switch (e.FeedItem.ActionType)
            {
                case LockActionType.VoteAdded:
                    OnNewAddVote?.Invoke(this, e);
                    OnNewVote?.Invoke(this, e);
                    break;

                case LockActionType.VoteRemoved:
                    OnNewRemoveVote?.Invoke(this, e);
                    OnNewVote?.Invoke(this, e);
                    break;

                case LockActionType.Voted:
                    OnNewVote?.Invoke(this, e);
                    break;

                case LockActionType.PilloryEntered:
                    OnPilloryEntered?.Invoke(this, e);
                    break;

                case LockActionType.PilloryAdded:
                    OnPilloryVoted?.Invoke(this, e);
                    break;

                case LockActionType.SelfAdded:
                    OnSelfAdded?.Invoke(this, e);
                    break;

                case LockActionType.ApiAdded:
                    OnApiAdded?.Invoke(this, e);
                    break;

                case LockActionType.ApiRemoved:
                    OnApiRemoved?.Invoke(this, e);
                    break;

                case LockActionType.WheelOfFortune:
                    OnWheelOfFortune?.Invoke(this, e);
                    break;

                case LockActionType.InsufficientGames:
                    OnInsufficientGames?.Invoke(this, e);
                    break;

                case LockActionType.RemainingDuration:
                    OnRemainingDuration?.Invoke(this, e);
                    break;

                case LockActionType.HolderChanged:
                    OnHolderChanged?.Invoke(this, e);
                    break;

                case LockActionType.HolderAdded:
                    OnHolderAdded?.Invoke(this, e);
                    OnHolderChanged?.Invoke(this, e);
                    break;

                case LockActionType.HolderRemoved:
                    OnHolderRemoved?.Invoke(this, e);
                    OnHolderChanged?.Invoke(this, e);
                    break;
            }
        }

        private void Run()
        {
            while (true)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    List<EmlalockFeedItem> result = _emlalockFeedReader.GetFeedItems().Result;
                    foreach (EmlalockFeedItem emlalockFeedItem in result)
                    {
                        if (FeedItems.TryAdd(emlalockFeedItem.ExternalId, emlalockFeedItem))
                        {
                            OnNewFeedItem?.Invoke(this, new EmlalockEventArgs(emlalockFeedItem));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }

    public class EmlalockEventArgs : EventArgs
    {
        public EmlalockFeedItem FeedItem
        {
            get;
        }

        public EmlalockEventArgs(EmlalockFeedItem feedItem)
        {
            FeedItem = feedItem;
        }
    }
}