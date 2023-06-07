public interface ICharacterHealth
{
    public void TakeDamage(int amount);
    public void Die();
    public int GetCurrentHealth();
    public int GetMaxHealth();
}